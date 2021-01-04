import React, { useState } from 'react'
import Footer from '../../../componentes/footer'
import Header from '../../../componentes/header'
import img from '../../../assets/img/LogoTipografia.png'


function ResetarSenha(){

    const [resp, setResp] = useState('')
    const [mail, setMail] = useState('')

    const gerarSenha = (email:string) => {
        fetch('http://localhost:5000/login/resetarSenha', {
            method:'post',
            body: JSON.stringify(email),
            headers: {
            'content-type': 'application/json'
            }
        }).then(re => re.json())
        .then(data => {
            if(data.msgsucesso !== undefined){
                setResp(data.msgsucesso)
                window.document.getElementById('box-paragrafo-p1')!.style.display = "block"
                window.document.getElementById('box-paragrafo-p2')!.style.display = "none"
                window.document.getElementById('box-paragrafo-p3')!.style.display = "none"



            }else if(data.msgerro !== undefined){
                setResp(data.msgerro)
                window.document.getElementById('box-paragrafo-p2')!.style.display = "block"
                window.document.getElementById('box-paragrafo-p1')!.style.display = "none"
                window.document.getElementById('box-paragrafo-p3')!.style.display = "none"


            }else if(data.msgsucesso1){
                setResp(data.msgsucesso1)
                window.document.getElementById('box-paragrafo-p3')!.style.display = "block"
                window.document.getElementById('box-paragrafo-p1')!.style.display = "none"
                window.document.getElementById('box-paragrafo-p2')!.style.display = "none"

            }
        }).catch(a => console.log(a))
    }




    return (
        <main>
                <Header descricao={""} />
                <div style={{height:"500px", width:"100%", display:"flex", justifyContent:"center", alignItems:"center", flexFlow:"column", padding:"30px"}}>
                    <img style={{width:"500px"}} src={img} />
                    <p>Coloque o seu E-Mail abaixo, uma nova senha será gerada e enviada para o seu E-Mail.</p>
                    <div>
                    <input style={{marginTop:"10px", marginRight:"5px", border:"solid 1px black"}} onChange={e => {setMail(e.target.value)}} type="email" placeholder="Digite o seu E-Mail" />
                        <button style={{height:"30px", width:"70px", cursor:"pointer"}} onClick={e => {e.preventDefault(); gerarSenha(mail)}} >Enviar</button>
                    </div>
                    
                    <div id="box-paragrafo-p1" style={{marginTop:"30px", backgroundColor:"blue", padding:"10px", width:"70%", textAlign:"center", display:"none"}}>
                        <p style={{color:"white"}}> {resp} </p>
                    </div>

                    <div id="box-paragrafo-p2" style={{marginTop:"30px", backgroundColor:"gray", padding:"10px", width:"70%", textAlign:"center", display:"none"}}>
                        <p style={{color:"white"}}> {resp} </p>
                    </div>

                    <div id="box-paragrafo-p3" style={{marginTop:"30px", backgroundColor:"black", padding:"10px", width:"70%", textAlign:"center", display:"none"}}>
                        <p style={{color:"white"}}> {resp} </p>
                    </div>

                </div>
                <Footer />
        </main>
    )
}

export default ResetarSenha