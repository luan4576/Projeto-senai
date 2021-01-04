import React, { useEffect, useState } from 'react';
import Header from '../../../componentes/header'
import Footer from '../../../componentes/footer'
import Vaga from '../../../componentes/vagas'
import user from '../../../assets/img/user.jpg'
import './style.css'


function CurriculoUsuario() {

    const [curriculo, setCurriculo] = useState<any>({})
    const [candidato, setnav] = useState<any>({})
    const [usuario, setUser] = useState<any>({})

    let [imagem, setImg] = useState('')


    useEffect(() => {
        buscarCurriculo()
    }, [])


    function dataConverter(value:string){
        var data = new Date(value)
        
        var mes = data.getMonth().valueOf() + 1;

        return ' ' + data.getDate()+ '/' + mes + '/' + data.getFullYear() 
    }


    const extrairUrl = () => {
        var url = window.location.href
        var idVaga = url.split('=')[1]
        return parseInt(idVaga)
    }


    const buscarCurriculo = () => {
        var idCandidato = extrairUrl()

        fetch('http://localhost:5000/bancoDeTalentos/curriculo/'+ idCandidato, {
            method:'get',
            headers:{
                "Content-Type": "application/json"
            }
        }).then(re => re.json())
        .then(data => {
            setCurriculo(data)
            setnav(data.idCandidatoNavigation)
            setUser(data.idCandidatoNavigation.idUsuarioNavigation)
            
            var imagem = data.idCandidatoNavigation.foto
            if (imagem === undefined || imagem === '') {
                setImg(user)
            } else {
                setImg('http://localhost:5000/imagens/' + imagem)
            }

        }).catch(a => console.log(a))
    }


    return (
        <div className="container">
            <Header descricao=""/>
            <section className="perfil">
                <div className="infoBasica">
                    <img src={imagem} className="userIcon" />
                    <h1>{candidato.nomeAluno}</h1>
                </div>
                <div className="infoCurso" style={{padding:"20px"}}>
                    <h3>E-Mail:</h3>
                    <p>{usuario.email}</p>
                    <h3>Data de nascimento:</h3>
                    <p>{dataConverter(candidato.dataNascimento)}</p>
                    <h3>Características:</h3>   
                    <p>{curriculo.palavraChave}</p>
                    <h3>Idioma estrangeiro:</h3>
                    <p>{curriculo.linguas}</p>




                    <div><p>.............</p></div>
                </div>
            </section>
            <section className="curriculo" style={{padding:"10px"}}>

             <textarea value={ curriculo.descricao+' \n \n \n \n ➤ Formação acadêmica: \n \n' +' ' +curriculo.cursosFormacoes + '\n \n  \n \n' } readOnly disabled style={{ resize: "none", padding: "10px", fontFamily: "verdana", fontSize: "15px", color: "black", height: "100%", width: "100%", borderRadius: "10px", backgroundColor: "white" }}>
              
              </textarea>


            </section>
            <Footer/>
        </div>
    )
}

export default CurriculoUsuario