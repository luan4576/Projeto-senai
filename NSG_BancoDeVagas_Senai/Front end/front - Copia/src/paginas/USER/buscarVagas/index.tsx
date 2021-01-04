import React, { useEffect, useState } from 'react';
import {Link, useHistory} from 'react-router-dom';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import './style.css'

function BuscarVagas()
{
    const [vaga, setVaga] = useState<any>([])
    const [pequisaParam, setPesquisa] = useState('')
    var hist = useHistory()
    
    const extrairPesquisa = () => {
        var url = window.location.href
        var pesquisa = url.split('=')[1]
        console.log(pesquisa)
        return pesquisa
    }

    function vagaPorId(id:''){
        hist.push('/descricaoVaga/id?='+ parseInt(id))
    }


    useEffect(() => {
        listarVagas()
        listarVagasPesquisa()
    }, [])

    const listarVagas = () => {
        let pesquisa = extrairPesquisa()

        if(pesquisa !== null){
            fetch('http://localhost:5000/vaga/pesquisa/' + pesquisa, {
                method:'get',
                headers:{
                    "Content-Type": "application/json",
                }
            }).then(resp => resp.json())
            .then(dados => {
                setVaga(dados)
                console.log(dados)
            }).catch(erro => console.log(erro))
        }
    }

    const listarVagasPesquisa = () => {
        let pesquisa = pequisaParam
        fetch('http://localhost:5000/vaga/pesquisa/'+ pesquisa, {
                method:'get',
                headers:{
                    "Content-Type": "application/json",
                }
            }).then(resp => resp.json())
            .then(dados => {
                setVaga(dados)
                console.log(dados)
            }).catch(erro => console.log(erro))
    }



    return(
        <div>
            <Header descricao="" />
            <section id="corpo-nv1">
                <div id="corpo-nv2-esquerdo">
                    <div id="corpo-nv3-esquerdo-info">
                        <form onSubmit={eve => {  eve.preventDefault(); listarVagasPesquisa() }}>

                            <h3>Pesquise uma oportunidade de emprego</h3>
                            <input onChange={eve => setPesquisa(eve.target.value)} placeholder="Pesquisar emprego" type="text" />
                        </form>
                    </div>
                    
                    <div id="corpo-nv3-esquerdo-info-inferior">
                        <button>Filtros</button>
                        <h4>Contrato</h4>

                        <div className="corpo-nv4-esquerdo-info-inferior-mini">
                            <input type="checkbox" /> <p>CLT</p>
                        </div>

                        
                        <div className="corpo-nv4-esquerdo-info-inferior-mini">
                            <input type="checkbox" /> <p>PJ</p>
                        </div>

                        
                        <div className="corpo-nv4-esquerdo-info-inferior-mini">
                            <input type="checkbox" /> <p>ESTÁGIO</p>
                        </div>
                    </div>


                </div>

                <div id="corpo-nv2-direito">

                {vaga.map((item:any) => {
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
                 )})}

                </div>

        </section>
        <Footer />
    </div>
    )
}

export default BuscarVagas;