import React, { useEffect, useState } from 'react';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import './style.css'
import Button from '../../../componentes/button';
import { Link, useHistory } from 'react-router-dom';

function EditarPerfilEmpresa(){

    const [empresa, setEmpresa] = useState<any>({})
    const [nomeFantasia, setNomeFantasia] = useState <any>('')
    const [razaoSocial, setRazaoSocial] = useState <any>('')
    const [cnae, setCnae] = useState <any>('')
    const [cnpj, setCnpj] = useState <any>('')
    const [imagemUsuario, setImag] = useState<any>()
    const [resposta, setResposta] = useState<any>()
    const [senha, setNovaSenha] = useState<any>('')


    let history = useHistory()


    useEffect(() => {
        chamarEmpresa()
    }, [])


    const cnpjMask = (value:any) => {
        return value
        .replace(/\D/g, '') 
        .replace(/^(\d{2})(\d)/,"$1.$2")
        .replace(/^(\d{2})\.(\d{3})(\d)/,"$1.$2.$3")
        .replace(/\.(\d{3})(\d)/,".$1/$2")
        .replace(/(\d{6})(\d)/,"$1")
    }

    const cnaeMask = (value:any) => {
        return value
        .replace(/\D/g, '') 
        .replace(/(\d{2})(\d)/,"$1")
    }

    const alterarSenha = () => {
        fetch('http://localhost:5000/empresa/alterarSenha', {
            method:'put',
            headers:{
                'Content-type':'application/json',
                Authorization:'bearer '+ localStorage.getItem('token')
            },
            body:JSON.stringify(senha)
        }).then(re => re.json())
        .then(dados => {
            if(dados.msgsucesso !== undefined){

                setResposta(dados.msgsucesso)
                window.document.getElementById('paragrafo03')!.style.display = "block"

            }else if(dados.msgerro !== undefined){

                setResposta(dados.msgerro)
                window.document.getElementById('paragrafo03')!.style.display = "block"

            }
        }).catch(e => console.log(e))
    }


    const chamarEmpresa = () => {

        fetch('http://localhost:5000/Empresa/Perfil', {
            method: 'get',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem("token")
            }
        })
            .then(resp => resp.json())
            .then(dados => {
                setEmpresa(dados)
            })
            .catch(erro => console.log(erro))
    }
   
   

    const editar = () => {
        const formAlteracao = {
            nomeFantasia:nomeFantasia,
           
            cnpj:cnpj,
            cnae:cnae,
            razaoSocial:razaoSocial
        }

        fetch('http://localhost:5000/empresa/perfil', {
            method:'put',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            },
            body:JSON.stringify(formAlteracao)
        })
        .then(re => re.json())
        .then( 
            data => {
                if(data.msgsucesso !== undefined){
                    alert(data.msgsucesso)
                    history.push('/empresa/meuPerfil')
                }else if(data.msgerro !== undefined){
                    alert(data.msgerro)
                }
            }
        ).catch(e => console.log(e))
    }


    const editarImagem = () => {
        const alteracao = new FormData()
        var file = new File(imagemUsuario, 'candidato.jpg')
        alteracao.append("foto", file)

        fetch('http://localhost:5000/empresa/perfil/imagem', {
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
        <div className="main">
            <Header descricao=""/>
         

            <div id="main-pb-flex-nv1">

                <h1>Alterar informações</h1>
                <form onSubmit={eve => {
                    eve.preventDefault()
                    editar()}}>

                        

                            <div className="campo">
                                <label className="label">Razão Social</label>
                                <Input className="input2" defaultValue={empresa.razaoSocial} onChange={e => setRazaoSocial(e.target.value)} name="Razão Social" minLength={5} maxLength={30} label="" />
                            </div>
                            <div className="campo">
                                <label className="label">Nome Fantasia</label>
                                <Input className="input2" defaultValue={empresa.nomeFantasia} onChange={e => setNomeFantasia(e.target.value)} minLength={5} maxLength={20} name="Nome fantasia" label="" />
                            </div>
                            <div className="campo">
                                <label className="label" htmlFor="">CNPJ</label>
                                <Input className="input2" placeholder={empresa.cnpj} onChange={e => setCnpj(cnpjMask(e.target.value))} value={cnpj} name="cpf" label="" />
                            </div>
                            <div className="campo">
                                <label className="label" htmlFor="">CNAE</label>
                                <Input className="input2" placeholder={empresa.cnae} onChange={e => setCnae(cnaeMask(e.target.value))} name="cpf" value={cnae} label="" />
                            </div>
                        
                            <button type="submit"> Alterar </button>
                </form>
                    

                <form onSubmit={eve => {eve.preventDefault(); editarImagem()}}> 
                    <h2>inserçao de img</h2>
                    <Input onChange={evento => setImag(evento.target.files)} type="file" className="textRequisitos" name="Razão Social" label=""/>

                    <button type="submit"> Alterar </button>
                </form>

                
                <form onSubmit={eve => {eve.preventDefault(); alterarSenha()}}> 
                    <h2>Alterar senha</h2>

                    <input onChange={e => {e.preventDefault(); setNovaSenha(e.target.value)}} style={{width:"300px", marginBottom:"20px", marginTop:"20px", border:"solid 1px black"}} placeholder="Digite uma nova senha" type="password"/>

                    <p id="paragrafo03" style={{fontFamily:'sans-serif', fontSize:'15px', display:'none', margin:"10px", color:"red"}}>{resposta}</p>


                    <button type="submit"> Alterar </button>
                </form>

            </div>
                
            <Footer />
      
        </div>
    )
}


export default EditarPerfilEmpresa;