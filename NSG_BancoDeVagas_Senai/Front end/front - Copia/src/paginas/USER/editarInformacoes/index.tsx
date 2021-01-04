import React, { useEffect, useState } from 'react';
import {Link, useHistory} from 'react-router-dom';
import Header from '../../../componentes/header/index';
import Footer from '../../../componentes/footer/index';
import Input from '../../../componentes/input/index';
import Button from '../../../componentes/button/index';
import './style.css'

function EditarInformacoes()
{
    const [candidato, setCandidato] = useState<any>({})
    const [nome, setNome] = useState<any>('')
    const [cpf, setCPF] = useState<any>('')
    const [nascimento, setNascimento] = useState<any>('')
    const [imagemUsuario, setImag] = useState<any>()
    const [senha,setNovaSenha] = useState<any>('') 
    const [resposta, setResposta] = useState<any>() 

    let history = useHistory()
    
    useEffect(() => {
        chamarUsr()
    }, [])


    const cpfMask = (value:any) => {
        return value
            .replace(/\D/g, '') 
            .replace(/(\d{3})(\d)/, '$1.$2') 
            .replace(/(\d{3})(\d)/, '$1.$2')
            .replace(/(\d{3})(\d{1,2})/, '$1-$2')
            .replace(/(-\d{2})\d+?$/, '$1') 
    }
        
   
    const alterarSenha = () => {
        
        fetch('http://localhost:5000/candidato/alterarSenha', {
            method:'put',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            },
            body:JSON.stringify(senha)
        }).then(re => re.json())
        .then(dados => {
            if(dados.msgsucesso !== undefined){

                setResposta(dados.msgsucesso)
                window.document.getElementById('paragrafo02')!.style.display = "block"

            }else if(dados.msgerro !== undefined){

                setResposta(dados.msgerro)
                window.document.getElementById('paragrafo02')!.style.display = "block"

            }
        }).catch(e => console.log(e))
    }


    const chamarUsr = () => {

        fetch('http://localhost:5000/candidato/curriculo', {
            method:'get',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
        .then(dados =>{
            setCandidato(dados.idCandidatoNavigation)
        })
        .catch(err => console.error(err))    
    }


    const editarInformacoes = () => {
        const formAlteracao = {
            nomeAluno: nome,
            cpf: cpf,
            dataNascimento: nascimento
        }

        fetch('http://localhost:5000/candidato', {
            method:'put',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            },
            body:JSON.stringify(formAlteracao)
        })
        .then( re => re.json()).then(data => {
            if(data.msgsucesso !== undefined){
                alert(data.msgsucesso)
                history.push('/perfilUsuario')
            }

        }).catch(data => alert(data.msgerro))
    }

    const editarImagem = () => {
        const alteracao = new FormData()
        var file = new File(imagemUsuario, 'candidato.jpg')
        alteracao.append("foto", file)

        fetch('http://localhost:5000/candidato/alterarFoto', {
            method:'put',
            headers:{
                authorization:'Bearer ' + localStorage.getItem('token')
            },
            body:alteracao
        }).then( () => {
            setImag(file)
            alert('Alterado com sucesso!')
            history.push('/perfilUsuario')
        }).catch(dt => alert(dt))
    }


    return(
        <div className="telaEditarInfo">
            <Header descricao=""/>
            <form onSubmit={eve => {eve.preventDefault(); editarInformacoes()}} className="formEditarInformacoes">
                <h2 className="textNomeCompleto">Nome Completo</h2>
                <Input className="spaceNomeCompleto" onChange={evento => setNome(evento.target.value)} defaultValue={candidato.nomeAluno} type="text" name="spaceNomeCompleto" label=""/>
                <br/>
                <br/>
                <h2 className="textDataNascimento">Data de Nascimento</h2>
                <Input className="spaceDataNascimento" onChange={evento => setNascimento(evento.target.value)} defaultValue={candidato.dataNascimento} type="date" name="spaceDataNascimento" label=""/>
                <br/>
                <br/>
                <h2 className="textCPF">CPF</h2>
                <Input className="spaceCPF" onChange={evento => setCPF(cpfMask(evento.target.value))} placeholder={candidato.cpf} value={cpf} name="spaceCPF" label=""/>
                
                <Button value="Salvar"/>
            </form>

            <form className="formEditarImg" onSubmit={eve => {eve.preventDefault(); editarImagem()}}>
                <h2 className="textAlterarImg">Alterar imagem</h2>
                <Input  className="spaceEditarImg" onChange={evento => setImag(evento.target.files)} name="imgUsuario" label="" type="file"/>
                <Button value="Salvar"/>
            </form>


            <form className="formEditarImg" style={{display:"flex", justifyContent:"center", flexDirection:"column", alignItems:"center"}} onSubmit={eve => {eve.preventDefault(); alterarSenha() }}>
                <h2 className="textAlterarImg">Alterar senha</h2>

                <input onChange={e => {e.preventDefault(); setNovaSenha(e.target.value)}} style={{width:"300px", marginBottom:"20px", marginTop:"20px", border:"solid 1px black"}} placeholder="Digite uma nova senha" type="password"/>

                <p id="paragrafo02" style={{fontFamily:'sans-serif', fontSize:'15px', display:'none', margin:"10px", color:"red"}}>{resposta}</p>

                <button type="submit" style={{cursor:"pointer",height:"40px", width:"100px", color:"white", backgroundColor:"black", border:"none", borderRadius:"10px"}} >Alterar</button>
            </form>

            <Footer/>
        </div>
    )
}

export default EditarInformacoes;