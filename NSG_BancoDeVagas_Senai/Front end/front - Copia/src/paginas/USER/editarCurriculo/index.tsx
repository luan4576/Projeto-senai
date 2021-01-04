import React, { useEffect, useState } from 'react';
import Header from '../../../componentes/header/index';
import Footer from '../../../componentes/footer/index';
import Input from '../../../componentes/input/index';
import Button from '../../../componentes/button/index';
import user from '../../../assets/img/user.jpg';
import './style.css'
import { useHistory } from 'react-router-dom';

function EditarCurriculo()
{
    const [candidato, setCandidato] = useState<any>({})


    let [imagem, setImg] = useState('')
    let history = useHistory()

    const [cursando, setCursando] = useState('')
    const [descricao, setDescricao] = useState('')
    const [experiencia, setXP] = useState('')
    const [escolaridade, setEscolaridade] = useState('')
    const [palavraChave, setPalavrasChave] = useState('')
    const [linguas, setLinguas] = useState('')


    useEffect(() => {
        chamarUsr()
    }, [])


    const chamarUsr = () => {

        fetch('http://localhost:5000/candidato/curriculo', {
            method:'get',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
        .then(dados =>{
            setCandidato(dados)
            
            let dataConvert = JSON.parse(JSON.stringify(dados.idCandidatoNavigation))
            
            if(dataConvert.foto === null || dataConvert.foto === undefined || dataConvert.foto === ''){
                setImg(user)
            }else{
                setImg('http://localhost:5000/imagens/' + dataConvert.foto)
            }

            console.log(dataConvert.foto)
        })
        .catch(err => console.error(err))   
    }


    const alterar = () => {
        const form = {
            cursando:Boolean(cursando),
            descricao:descricao,
            cursosFormacoes: experiencia,
            escolaridade: escolaridade,
            palavraChave: palavraChave,
            linguas: linguas
        }

        
    fetch('http://localhost:5000/candidato/curriculo', {
        method:'put',
        headers: {
            'content-type':'application/json',
            authorization:'Bearer ' + localStorage.getItem('token')
        },
        body:JSON.stringify(form)
    })
    .then( re => re.json())
    .then(data => {
        if(data.msgsucesso !== undefined){
            alert(data.msgsucesso)
            history.push('/perfilUsuario')
        }
    }).catch(data => alert(data.msgerro))

    }

    
    return(
        <div className="telaEditarCurriculo">
            <Header descricao=""/>
            <section className="Dados">

                <form onSubmit={evento => {evento.preventDefault(); alterar()}}>

                <h1 className="titulo1">Descrição</h1>

                    <textarea className="spaceDesc" style={{ padding:'10px', height:"200px"}} onChange={evento => setDescricao(evento.target.value)}  defaultValue={candidato.descricao} ></textarea>
                   
                    <h1 className="titulo2">Escolaridade</h1>
                    <textarea className="spaceExp" style={{ padding:'10px', height:"200px"}} onChange={evento => setXP(evento.target.value)} defaultValue={candidato.cursosFormacoes} ></textarea>


                    <img style={{margin:"30px"}} className="fotoUser" src={imagem} alt="Foto do usuário candidato"/>

                    <h1 className="textOpCurso">Cursando?</h1>

                    <div className="formOpCurso">
                            <select id="cars" onChange={e => setCursando(e.target.value)}>
                                <option value="true">Sim</option>
                                <option value="false">Não</option>
                            </select>
                    </div>
                    
                    <h2 className="textPalavraChave">Palavras chaves</h2>
                    <Input className="spacePalavraChave" onChange={evento => setPalavrasChave(evento.target.value)}  defaultValue={candidato.palavraChave} label="" name="spacePalavraChave"/>

                    <h2  className="textLinguas">Línguas</h2>
                    <Input className="spaceLinguas" onChange={evento => setLinguas(evento.target.value)} defaultValue={candidato.linguas} label="" name="spacePalavraChave"/>

                    <br/>


                    <Button value="Salvar"/>

                </form>
            </section>
          
        </div>
    )
}

export default EditarCurriculo;