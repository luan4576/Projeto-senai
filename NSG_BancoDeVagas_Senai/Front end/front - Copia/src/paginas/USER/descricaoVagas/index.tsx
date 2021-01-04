import React, { useEffect, useState } from 'react';
import Header from '../../../componentes/header'
import Footer from '../../../componentes/footer'
import user from '../../../assets/img/enterprise.png'
import Button from '../../../componentes/input/index'
import './style.css'


function DescricaoVagas() {

const [tipoVaga, setTipoVaga] = useState<any>({})
const [empresa, setEmpresa] = useState<any>({})
const [vaga, setVaga] = useState<any>({})
const [estadoVaga, setEstadoVaga] = useState<any>()

let [imagem, setImg] = useState('')



const extrairUrl = () => {
    var url = window.location.href
    var idVaga = url.split('=')[1]
    return parseInt(idVaga)
}


useEffect(() => {
    verificarCandidatura()
    chamarVaga()
}, [])  

const candidatura = () => {
    let id = extrairUrl()

    const form = {
        idVaga:id
    }

    if(id != null){

        fetch('http://localhost:5000/candidatura', {
            method:'post',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            },
            body:JSON.stringify(form)
        })
        .then(res => res.json())
        .then(dados => {
            console.log(dados)
            if(dados.msgsucesso !== undefined){
                alert(dados.msgsucesso)
                window.location.reload()
            }
            else if(dados.msgerro !== null || dados.msgerro !== undefined){
                alert(dados.msgerro)
            }

        })
    }
}


const verificarCandidatura = () => {

    

    fetch('http://localhost:5000/candidato/candidaturaValida/'+ extrairUrl(), {
            method:'get',
            headers:{
                'Content-Type': 'application/json',
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        })
        .then(re => re.json())
        .then(dados => {
            console.log(dados)

            if(dados.msgsucesso !== undefined){
                setEstadoVaga(dados.msgsucesso)
                var botao = window.document.getElementById('botao01')!
                botao.style.display = "none"
                
            }else if(dados.msgsucesso2 !== undefined){
                setEstadoVaga(dados.msgsucesso2)
                var paragrafo = window.document.getElementById('paragrafo01')!
                paragrafo.style.display = "none"
            }
        })
        .catch(e => console.log(e))
} 

const chamarVaga = () => {
    var id = extrairUrl()
    if(id != null){
        fetch('http://localhost:5000/vaga/' + id, {
            method:'get',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
        .then(dados => {
            setVaga(dados)
            setEmpresa(dados.idEmpresaNavigation)
            setTipoVaga(dados.idTipoVagaNavigation)

            var imagem = dados.idEmpresaNavigation.imagemEmpresa
            if (imagem === undefined || imagem === '') {
                setImg(user)
            } else {
                setImg('http://localhost:5000/imagens/' + imagem)
            }

            console.log(dados)
        }).catch(erro => alert(erro))
    }
    
}

    return (
        <div>
            <Header descricao="" />
            <div style={{marginBottom:"200px"}}>

            <section className="empresa">
                <div className="infoUsuario">
                    <img className="iconEmpresa" src={imagem} />
                    <div className="texto">
                        <h1>{empresa.nomeFantasia}</h1>
                    </div>
                </div>
            </section>
            <section className="infoVaga">


                <div id="vagaInfo-nv1">
                    <h1>{vaga.titulo}</h1>
                    <p><strong>Salário:</strong> R$ {vaga.salario}</p>
                    <p><strong>Tipo de Contrato:</strong> {tipoVaga.nomeTipoVaga}</p>
                    <p><strong>Palavras Chave:</strong> {vaga.palavraChave}</p>
                </div>


                <div id="vagaInfo-nv1-inferior">
                    <p><strong>Descrição: </strong>  { '\n \n' + vaga.descricao}</p>

                    <button onClick={eve => {
                        eve.preventDefault()
                        candidatura()
                    }} id="botao01" style={{
                        textAlign:"center",height:"40px", width:"150px", marginTop:"20px", borderRadius:"10px", 
                        backgroundColor:"red", color:"white", border:"none", padding:"5px", cursor:"pointer"}} > {estadoVaga} </button>
                    
                    <p id="paragrafo01" style={{fontFamily:"verdana", fontSize:"15px", color:"red", padding:"10px"}}> {estadoVaga} </p>

                </div>


            </section>
            </div>
            <Footer/>
        </div>
    )
}

export default DescricaoVagas