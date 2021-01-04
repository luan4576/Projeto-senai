import React, { useEffect, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import Header from '../../../componentes/header/index';
import Footer from '../../../componentes/footer/index';
import Input from '../../../componentes/input/index';
import Button from '../../../componentes/button/index';
import tecnico from '../../../assets/img/tecnico.jpeg';
import empresas from '../../../assets/img/empresas.png';
import user from '../../../assets/img/enterprise.png';

import './style.css'
import { parseJwt } from '../../../service/auth';

function Home() {
    
    const [vaga, setVaga] = useState<any>([])
    let [pesquisa, setPesquisa] = useState('');
    let history = useHistory()

    useEffect(() => {
        listarVagas()
        carregar()
    }, [])

    
    const pesquisarVaga = () => {
        setPesquisa('')
        history.push('/buscarVagas?param=' + pesquisa);
    }

    function vagaPorId(id:''){
        history.push('/descricaoVaga/id?='+ parseInt(id))
    }

    function trazerImg(imagem:string){
        if(imagem === undefined){
            return user
        }   
        return 'http://localhost:5000/imagens/' + imagem
    }


    const listarVagas = () => {
        fetch('http://localhost:5000/vaga', {
            method: 'get',
            headers: {
                "Content-Type": "application/json"
            }
        }).then(resp => resp.json())
            .then(dados => {
                setVaga(dados)
            }).catch(erro => console.log(erro))
    }

    function carregar(){
        
        var tokenUsr = localStorage.getItem('token') 

        if( tokenUsr === null ||  tokenUsr === undefined || tokenUsr === ''){
            console.log('Cadastre-se no sistema!')
        }else{
            const elemento = window.document.getElementById('centro-direito-flex-btn')!
            elemento.style.display = 'none'
        }

    }



    return (
        <div>
            <Header descricao="" />
            <body>
                <section className="home">
                        <div className="contentSearch">

                            <form className="formSearch" onSubmit={e => { pesquisarVaga() }}>

                                <div className="spaceSearch" >
                                    <h3>Procure uma vaga</h3>
                                    <Input name="space" label="" onChange={eve => setPesquisa(eve.target.value)} placeholder="Pesquise uma vaga" />
                                </div>
                            </form>


                            <div className="images">
                                <img className="senai" src={tecnico} alt="Imagem de curso técnico" />
                                <img className="senai" src={empresas} alt="Imagem do aplicativo do Senai conectado às empresas" />
                            </div>



                        </div>


                        <div className="cadastre-se">
                            <div id="centro-direito-flex-btn">
                                <button className="btnCadastrar" onClick={() => history.push('/cadastroEmpresa')} >Cadastre sua empresa!</button>
                                <button className="btnCadastrar" onClick={() => history.push('/cadastro')}>Cadastre seu currículo</button>
                            </div>

                            <h1 style={{ marginBottom: "5px", textAlign: "center" }}>Temos vaga de</h1>
                            {vaga.map((item: any) => {
                                return (
                                    <div style={{ margin: "20px", marginTop: "60px", marginBottom: "100px" }} >
                                        <p onClick={() => vagaPorId(item.idVaga)}><img style={{ width: "100px", marginRight: "20px" }} src={trazerImg(item.idEmpresaNavigation.imagemEmpresa)} />
                                            <strong>{item.titulo}</strong> - {item.idEmpresaNavigation.nomeFantasia}</p>
                                    </div>
                                )
                            })}

                        </div>
                </section>
            </body>
            <Footer />
        </div>
    )
}

export default Home;