import React, { useEffect, useState } from 'react';
import {Link, useHistory} from 'react-router-dom';
import Header from '../../componentes/header/index';
import Footer from '../../componentes/footer/index';
import Input from '../../componentes/input/index';
import Button from '../../componentes/button/index';
import tecnico from '../../assets/img/tecnico.jpeg';
import empresas from '../../assets/img/empresas.png';
import './style.css'
import { parseJwt } from '../../service/auth';

function Home()
{    
    const [vaga, setVaga] = useState<any>([])

    let [pesquisa, setPesquisa] = useState('');

    let history = useHistory()

    const pesquisarVaga = () => {
        setPesquisa('')
        history.push('/buscarVagas?param=' + pesquisa);
    }   

    useEffect(() => {
        listarVagas()
    }, [])

    const listarVagas = () => {
        fetch('http://localhost:5000/vaga', {
            method:'get',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
        .then(dados => {
            
            console.log(parseJwt().role)

            setVaga(dados)
            console.log(dados)
        }).catch(erro => console.log(erro))
    }
    


    return(
        <div>
            <Header descricao=""/>
            <section style={{display:"flex", height:"100%", width:"100%"}}>
            <div style={{width:"50%"}}>
                <h2 className="teste">Procure aqui uma vaga</h2>

                <form onSubmit={e => {pesquisarVaga()}}>
                    <Input className="spaceSearch" name="space" label="" onChange={eve => setPesquisa(eve.target.value)} placeholder="Pesquise uma vaga" /> 
                </form>


                <div className="images">
                    <img src={tecnico} alt="Imagem de curso técnico"/>
                    <img className="emp" src={empresas} alt="Imagem do aplicativo do Senai conectado às empresas"/>
                </div>
            </div>
            <div style={{width:"50%", marginTop:"300px", padding:"30px", fontSize:"50px"}}>
                <h1 style={{marginBottom:"30px"}}>Temos vaga de</h1>
                {vaga.map((item:any) => {
                    return (
                        <div style={{margin:"20px"}} >
                        <p><img style={{width:"100px", marginRight:"20px"}} src={"http://localhost:5000/imagens/"+ item.idEmpresaNavigation.imagemEmpresa} />
                        <strong>{item.titulo}</strong> - {item.idEmpresaNavigation.nomeFantasia}</p>
                        </div>
                    )
                })}
            </div>
            </section>
            <Footer/>
        </div>
        
    )
}

export default Home;