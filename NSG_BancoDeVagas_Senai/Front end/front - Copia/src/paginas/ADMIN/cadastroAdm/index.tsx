import React, { useState } from 'react';
import Header from '../../../componentes/header'
import Footer from '../../../componentes/footer'
import Button from '../../../componentes/button'
import './style.css'

function CadastroAdm() {


const [email,setEmail] = useState  ('')
const [senha,setSenha] = useState ('')

const CadastroAdm=()=>{
    const Administrador = {
        email:email,
        senha:senha
    }
fetch('http://localhost:5000/administrador/cadastrarAdministrador',{
method:'POST',
headers:{
    'content-type':'application/json',
    authorization: 'Bearer ' + localStorage.getItem('token')},

body : JSON.stringify(Administrador)

})
.then(re => re.json())
.then(
    data=>{
        if(data.msgsucesso === undefined){
alert('ocorreu um erro ao se cadastrar')
    }
        alert(data.msgsucesso)
    }
)

}


    return (
        <div className="cadastroAdm">
            <Header descricao="" />
            <div className="background">
                <section className="inputBg">
                    <h1 className="titulo">Cadastrar Adm</h1>
                    <form onSubmit={eve=>{eve.preventDefault();CadastroAdm()}}>
                        <div className="campo">
                            <label  className="label">Email</label>
                            <input onChange={e => setEmail(e.target.value)} className="input2" type="text"></input>
                        </div>
                        <div className="campo">
                            <label className="label">Senha</label>
                            <input onChange={e => setSenha(e.target.value)} className="input2" type="password"></input>
                        </div>
                        <div className="botao">
                            <Button value="Cadastrar" />
                        </div>

                    </form>

                </section>
            </div>
            <Footer />
        </div>

    )
}

export default CadastroAdm;