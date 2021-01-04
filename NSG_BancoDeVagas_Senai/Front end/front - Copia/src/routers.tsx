import {BrowserRouter, Redirect, Route} from 'react-router-dom';
import Home from './paginas/PUBLICO/home';
import React from 'react';
import Cadastro from './paginas/USER/cadastro/index';
import Login from './paginas/PUBLICO/login/index';
import BuscarVagas from './paginas/USER/buscarVagas/index';
import MinhasCandidaturas from './paginas/USER/minhasCandidaturas/index';
import PerfilUsuario from './paginas/USER/perfilUsuario';
import EditarCurriculo from './paginas/USER/editarCurriculo';
import PerfilEmpresa from './paginas/EMPRESA/perfilEmpresa';
import PerfilDaEmpresa from './paginas/USER/perfilDaEmpresa'
import EditarInformacoes from './paginas/USER/editarInformacoes';
import CadastroVaga from './paginas/EMPRESA/cadastroVaga';
import MinhasVagas from './paginas/EMPRESA/minhasVagas';
import CurriculoUsuario from './paginas/EMPRESA/curriculoUsuario';
import DescricaoVaga from './paginas/USER/descricaoVagas';
import CadastroAdm from './paginas/ADMIN/cadastroAdm';
import CadastroEmpresa from './paginas/EMPRESA/CadastroEmpresa';
import ListagemCandidatosVaga from './paginas/EMPRESA/listagemCandidatos';
import ListagemEstagios from './paginas/ADMIN/ListagemEstagios';
import BancoDeTalentos from './paginas/EMPRESA/bancoDeTalentos/index'
import AtualizaPerfilEmpresa from './paginas/EMPRESA/AtualizarPerfilEmpresa/index'
import resetarSenha from './paginas/PUBLICO/resetarSenha/resetarSenha'
import { parseJwt } from './service/auth';

function Routers()
{
    const RotaPrivadaCandidato = ({Componente, rota,...rest } : any) => (
        <Route  
            render={props => 
                localStorage.getItem('token') !== null && parseJwt().role === 'Candidato' ? (
                    <Componente {...props} />
                    
                ) : (
                    <Redirect   
                        to={{pathname:"/login"}}
                    />
                )
            }

            path={rota}
        />
    )


    const RotaPrivadaAdm = ({Component, rota, ...rest } : any) => (
        <Route  
            render={props => 
                localStorage.getItem('token') !== null && parseJwt().role === 'Administrador' ? (
                    <Component {...props} />
                ) : (
                    <Redirect   
                        to={{pathname:"/login"}}
                    />
                )
            }
            path={rota}
        />
    )

    const RotaPrivadaEmpresa = ({Component, rota, ...rest } : any) => (
        <Route  
            render={props => 
                localStorage.getItem('token') !== null && parseJwt().role === 'Empresa' ? (
                    <Component {...props} />
                ) : (
                    <Redirect   
                        to={{pathname:"/login"}}
                    />
                )
            }

            path={rota}
        />
    )

    function verificacao(){
        if(parseJwt().role === 'Empresa'){
            return '/perfilEmpresa'
        }else if(parseJwt().role === 'Candidato'){
            return '/perfilUsuario'
        }else{
            return '/login'
        }
    }

    const RotaLogin = ({Component, rota, ...rest } : any) => (

        <Route  
            render={ props =>
                localStorage.getItem('token') !== null ? (

                    <Redirect   
                    to={{pathname:verificacao()}}
                    />
                ) : (
                    <Component {...props} /> 
                )
            }

            path={rota}
        />
    )

    
    return(
        <BrowserRouter>

            <Route path="/login" component={Login}/>

            <Route exact={true} path="/" component={Home}/>
            <Route path="/home" component={Home}/>
			<Route path="/cadastro" component={Cadastro}/>
            <Route path="/cadastroEmpresa" component={CadastroEmpresa}/>
            <Route path="/buscarVagas" component={BuscarVagas}/>
            <Route path="/resetarSenha" component={resetarSenha}/>


            <RotaPrivadaCandidato Componente={MinhasCandidaturas} rota={'/minhasCandidaturas'} />
            <RotaPrivadaCandidato rota="/perfilUsuario" Componente={PerfilUsuario}/>
            <RotaPrivadaCandidato rota="/editarCurriculo" Componente={EditarCurriculo}/>
            <RotaPrivadaCandidato rota="/editarInformacoes" Componente={EditarInformacoes}/>
            <RotaPrivadaCandidato rota="/descricaoVaga" Componente={DescricaoVaga}/>
            
            
            <RotaPrivadaEmpresa rota="/empresa/meuperfil" Component={PerfilEmpresa}/>
            <RotaPrivadaEmpresa rota="/empresa/atualizar" Component={AtualizaPerfilEmpresa}/>
            <RotaPrivadaEmpresa rota="/empresa/minhasVagas" Component={MinhasVagas}/>
            <RotaPrivadaEmpresa rota="/empresa/cadastroVaga" Component={CadastroVaga}/>
            <RotaPrivadaEmpresa rota="/empresa/listagemCandidatosVaga" Component={ListagemCandidatosVaga}/>
            <RotaPrivadaEmpresa rota="/curriculoDoUsuario" Component={CurriculoUsuario}/>
            <RotaPrivadaEmpresa rota="/bancoDeTalentos" Component={BancoDeTalentos}/>


            <RotaPrivadaAdm rota="/administrador/estagioPorId" Component={PerfilDaEmpresa}/>
            <RotaPrivadaAdm rota="/administrador/listagemEstagio" Component={ListagemEstagios}/>
            <RotaPrivadaAdm rota="/administrador/cadastroAdm" Component={CadastroAdm}/>
            
        </BrowserRouter>
    )
}

export default Routers;