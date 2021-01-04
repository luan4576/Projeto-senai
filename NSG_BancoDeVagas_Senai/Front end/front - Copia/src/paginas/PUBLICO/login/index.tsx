import React, { useState } from 'react';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import Button from '../../../componentes/button/index'
import './style.css'
import recepcao from '../../../assets/img/recepcao.jpg'

import logo from '../../../assets/img/LogoTipografia.png'

import { useHistory } from 'react-router-dom'

function Login() {


  let history = useHistory();

  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');

  const login = () => {
    const login = {
      email: email,
      senha: senha
    }


    fetch('http://localhost:5000/Login',
      {
        method: 'POST',
        body: JSON.stringify(login),
        headers: {
          'content-type': 'application/json'
        }

      })

      .then(response => response.json())
      .then(dados => {

        if (dados.token !== undefined) {
          localStorage.setItem('token', dados.token)
          history.push('/home');
        } else {
          alert(dados.msgerro)
        }

      })

      .catch(erro => console.error(erro))
  }




  return (
    <div >
      <Header descricao="" />
      <div className="telaLogin">
        <div className="greyBg">

          <div className="input">
            <form onSubmit={event => {
              event.preventDefault();
              login();
            }}>
              <div className="pr">
                <h2 className="textEmail">Email</h2>
                <Input type="email" className="email" label="" name="btnEntrar" onChange={e => setEmail(e.target.value)} />
              </div>
              <div>
                <h2 className="textSenha">Senha</h2>
                <Input type="password" className="senha" label="" name="btnEntrar" onChange={e => setSenha(e.target.value)} />
              </div>

              <p onClick={() => history.push('/resetarSenha')} className="txtVermelho" >Esqueci minha senha</p>
              <p onClick={() => history.push('/cadastro')} className="txtVermelho">Cadastre-se</p>


              <Button value="Entrar" />


            </form>       

          </div>

          <hr className="divisor" />
          <img src={logo} className="logoBanco" />

          
        </div>
      </div>


      <Footer />


    </div>
  )
}

export default Login;