import React, { useEffect, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import './style.css'
import Button from '../../../componentes/button';

function MinhasCandidaturas() {
    const [candidaturas, setCandidaturas] = useState([])

    useEffect(() => {
        ChamarCandidaturas()
    }, [])

    let hist = useHistory()

    function vagaPorId(id: '') {
        hist.push('/descricaoVaga/id?=' + parseInt(id))
    }

    function dataConverter(value: string) {
        var data = new Date(value)

        var mes = data.getMonth().valueOf() + 1;

        return ' ' + data.getDate() + '/' + mes + '/' + data.getFullYear()
    }

    const ChamarCandidaturas = () => {
        fetch('http://localhost:5000/candidato/minhasCandidaturas', {
            method: 'get',
            headers: {
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
            .then(data => {
                setCandidaturas(data)
            }).catch(a => console.log(a))
    }

    return (
        <div className="telaMinhasCandidaturas">
            <Header descricao="" />
            <h1 className="textMyVagas">Minhas Candidaturas</h1>


            <section className="corpo-nv1-exclusivo" >

                {candidaturas.map((item: any) => {

                    return (
                        <div className="coluna-candidatura">
                            <div className="conteudo-vaga" onClick={() => vagaPorId(item.idVagaNavigation.idVaga)}>
                                <div id="corpo-nv2-direito-conteudo-info">
                                    <p>Data da candidatura {dataConverter(item.dataCandidatura)}</p>
                                </div>
                                <div id="corpo-nv2-direito-conteudo-titulo">
                                    <p>{item.idVagaNavigation.titulo}</p>
                                </div>
                            </div>
                        </div>

                    )
                })}

            </section>


            <Footer />
        </div>
    )
}

export default MinhasCandidaturas;