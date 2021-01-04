import React, { useEffect, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import './style.css'
import Button from '../../../componentes/button';

function Cadastro() {
    const [nome, setNome] = useState('')
    const [email, setEmail] = useState('')
    const [senha, setSenha] = useState('')
    const [cpf, setCpf] = useState('')

    let history = useHistory()

    const cpfMask = (value:any) => {
        return value
            .replace(/\D/g, '') 
            .replace(/(\d{3})(\d)/, '$1.$2') 
            .replace(/(\d{3})(\d)/, '$1.$2')
            .replace(/(\d{3})(\d{1,2})/, '$1-$2')
            .replace(/(-\d{2})\d+?$/, '$1') 
        }
    
    const salvar = () => {
        const form = {
            nomeAluno: nome,
            email: email,
            senha: senha,
            cpf: cpf
        }

        fetch('http://localhost:5000/candidato', {
            method: 'post',
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(form)
        })
            .then(re => re.json())
            .then(data => {
                if(data.msgsucesso !== undefined){
                    alert(data.msgsucesso)
                    history.push('/home')
                }else if(data.msgerro !== undefined){
                    alert(data.msgerro)
                }
            })
            .catch(data => console.log(data))
    }








    return (
        <div className="main">
            <Header descricao="" />
            <div className="background">
                <section className="inputBg">
                    <h1>Cadastre-se</h1>
                    <form onSubmit={eve => {
                        eve.preventDefault()
                        salvar()
                    }} >
                        <div className="campo">
                            <label className="label">Nome</label>
                            <Input className="input2" value={nome} onChange={e => setNome(e.target.value)} placeholder="Digite o seu nome" name="nome" label="" />
                        </div>
                        <div className="campo">
                            <label className="label">Email</label>
                            <Input className="input2" value={email} type="email" placeholder="Endereço de E-Mail" onChange={e => setEmail(e.target.value)} name="senha" label="" />
                        </div>
                        <div className="campo">
                            <label className="label" htmlFor="">CPF</label>
                            <Input className="input2" value={cpf} placeholder="000.000.000-00" onChange={e => setCpf(cpfMask(e.target.value))} name="cpf" label="" />
                        </div>
                        <div className="campo">
                            <label className="label">Senha</label>
                            <Input className="input2" value={senha} onChange={e => setSenha(e.target.value)} name="senha" label="" type="password"/>
                        </div>
                        <Button value="Cadastrar" />
                        
                    </form>
                </section>

            </div>
            
            <Footer />
        </div>
    )
}

export default Cadastro;