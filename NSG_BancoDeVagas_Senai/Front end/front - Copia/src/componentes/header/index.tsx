import React from 'react';
import './style.css';
import '../../assets/style/global.css';
import {Link,useHistory} from 'react-router-dom';
import logoSenai from '../../assets/img/logoSenai.png';
import {parseJwt} from '../../../src/service/auth'

interface HeaderProps{
    descricao: string;
}
const Header:React.FC<HeaderProps> =(props) => {

    let history = useHistory()

    const logout = () =>{
      localStorage.removeItem('token');
      history.push('/login')
    }
   
    const menu = () => {
        const token = localStorage.getItem('token')
        if(token === undefined || token === null){
            return(
                <div id="botoesheader">
                <button id="btnheader" onClick={() => history.push('/login')}>Entrar</button>
            </div>
            )
        }else if(parseJwt().role === "Empresa"){
            return(
            <div id="botoesheader">
                <ul id="nav"> 
                        <li><a href="#">Ações</a> 
                            <ul> 
                                <li><a><Link to="/empresa/meuperfil">Perfil</Link></a></li>
                                <li><a><Link to="/empresa/minhasVagas">Minhas vagas</Link></a></li>
                                <li><a style={{cursor:"pointer"}} onClick={() => logout()}>Sair</a></li> 
                            </ul> 
                        </li>
                </ul>
            </div>)
        }else if(parseJwt().role === "Candidato"){
            
            return(
            <div id="botoesheader">
                <ul id="nav"> 
                        <li><a href="#">Ações</a> 
                            <ul> 
                                <li><a><Link to="/perfilUsuario">Perfil</Link></a></li>
                                <li><a><Link to="/minhasCandidaturas">Candidaturas</Link></a></li>
                                <li><a style={{cursor:"pointer"}} onClick={() => logout()}>Sair</a></li> 
                            </ul> 
                        </li>
                </ul>
            </div>
            )
        }else if(parseJwt().role === "Administrador"){
            return(
            <div id="botoesheader">
                <ul id="nav"> 
                        <li><a href="#">Admin</a> 
                            <ul> 
                                <li><a><Link to="/administrador/listagemEstagio">Estagios</Link></a></li>
                                <li><a><Link to="/administrador/listagemEstagio">Cadastrar adm</Link></a></li>
                                <li><a style={{cursor:"pointer"}} onClick={() => logout()}>Sair</a></li> 
                            </ul> 
                        </li>
                </ul>
            </div>
            )
        }else{
            return(
            <div id="botoesheader">
                <button id="btnheader" onClick={event=>{event.preventDefault();}}>Entrar</button>
            </div>
            )
        }
    }
   
    return(
        <div className="header">
            <div>
            <img className="logoSenai" src={logoSenai} style={{cursor:"pointer"}} onClick={() => history.push('/home')} alt="Logo Senai"/>
            <p>{props.descricao}</p>
            </div>
            
            {menu()}
            
        </div>

        
    )
}

export default Header;