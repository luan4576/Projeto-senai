import React, { useEffect, useState } from 'react';
import {Link, useHistory} from 'react-router-dom';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import './style.css'
import Button from '../../../componentes/button';

function MinhasVagas()
{
    const [vagas, setVagas] = useState<any>([])

    useEffect(() => {
        listarMinhasVagas()
    }, [])

    var hist = useHistory()

    function vagaPorId(id:''){
        hist.push('/empresa/listagemCandidatosVaga/id?='+ parseInt(id))
    }

    const listarMinhasVagas = () => {
        fetch('http://localhost:5000/empresa/vagas', {
            method:'get',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(re => re.json())
        .then(data => {
            console.log(data)
            setVagas(data)
        }).catch(a => console.log(a))
    }

    return(
        <div className="telaMinhasVagas">
            <Header descricao=""/>
            <h1 className="textMyVagas">Minhas Vagas</h1>
            <div style={{display:"flex", flexDirection:"column", width:"100%", justifyContent:"center", alignItems:"center", height:"100%", marginBottom:"300px", textAlign:"center", padding:"20px"}}>
            {vagas.map((item:any) => {
                return(
                        
                        <div id="corpo-nv2-direito-conteudo" onClick={() => vagaPorId(item.idVaga)}>
                            <div id="corpo-nv2-direito-conteudo-info">
                                <p>Empresa: {item.idEmpresaNavigation.nomeFantasia}</p> 
                                <p>R$ {item.salario}</p>
                            </div>  
                            <div id="corpo-nv2-direito-conteudo-titulo">
                            <p>{item.titulo}</p>
                            </div>
                        </div>
                )
            })}
            
            </div>

            <Footer/>
        </div>
    )
}

export default MinhasVagas;