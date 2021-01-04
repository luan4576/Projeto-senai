import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import './style.css'
import Button from '../../../componentes/button';

function CadastroEmpresa() {

    const [email, setEmail] = useState('')
    const [senha, setSenha] = useState('')
    const [nomeFantasia, setNomeFantasia] = useState('')
    const [razaoSocial, setRazaoSocial] = useState('')
    const [cnae, setCnae] = useState('')
    const [cnpj, setCnpj] = useState('')

    let history = useHistory()

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

    const salvar = () => {
        const form = {
            nomeFantasia: nomeFantasia,
            email: email,
            senha: senha,
            cnpj: cnpj,
            cnae: cnae,
            razaoSocial: razaoSocial
        }

        fetch('http://localhost:5000/Empresa', {
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
                    history.push('/login')
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

                <section className="inputBgG">
                    <h1>Cadastre a sua empresa!</h1>
                    <form onSubmit={eve => {
                        eve.preventDefault()
                        salvar()
                    }}>

                        <div className="campo">
                            <label className="label">Razão Social</label>
                            <Input className="input2" value={razaoSocial} onChange={e => setRazaoSocial(e.target.value)} name="Razão Social" placeholder="Ex: Empresa SA" label="" maxLength={30} />
                        </div>
                        <div className="campo">
                            <label className="label">Nome Fantasia</label>
                            <Input className="input2" value={nomeFantasia} onChange={e => setNomeFantasia(e.target.value)} name="Nome fantasia" label="" placeholder="Ex: Empresa" maxLength={20} />
                        </div>
                        <div className="campo">
                            <label className="label" htmlFor="">CNPJ</label>
                            <Input className="input2" value={cnpj} onChange={e => setCnpj(cnpjMask(e.target.value))} placeholder="00.000.000/0000-00" name="cpf" label="" />
                        </div>
                        <div className="campo">
                            <label className="label" htmlFor="">CNAE</label>
                            <Input className="input2" value={cnae} onChange={e => setCnae(cnaeMask(e.target.value))} placeholder="00"  name="cpf" label="" />
                        </div>
                        <div className="campo">
                            <label className="label" htmlFor="">Email</label>
                            <Input className="input2" value={email} type="email" placeholder="Digite o seu E-Mail" onChange={e => setEmail(e.target.value)} name="cpf" label="" />
                        </div>
                        <div className="campo">
                            <label className="label">Senha</label>
                            <Input className="input2" value={senha} onChange={e => setSenha(e.target.value)} name="senha" label="" type="password" />
                        </div>
                        <Button value="Cadastrar" />

                    </form>
                </section>
            </div>


            <Footer />
        </div>
    )
}

export default CadastroEmpresa;