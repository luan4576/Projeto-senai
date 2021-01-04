import React, { useEffect, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import './style.css'
import Button from '../../../componentes/button';

function CadastroVaga() {

    const [titulo, setTitulo] = useState('')
    const [descricao, setDescricao] = useState('')
    const [palavraChave, setPalavraChave] = useState('')
    const [endereco, setEndereco] = useState('')
    const [salario, setSalario] = useState('')
    const [idTipoCurso, setIdTipoCurso] = useState('')
    const [idTipoVaga, setIdTipoVaga] = useState('')


    const [listaTipoVaga, setListaTipoVaga] = useState([])
    const [listaTipoCurso, setListaTipoCurso] = useState([])

    useEffect(() => {
        listar()
        listarTipoCurso()
    }, [])



    const listar = () => {
        fetch('http://localhost:5000/empresa/tiposvaga', {
            method: 'GET',
            headers: {
                //Bearer authentication é o token authentication, um Schema para autenticação HTTP 
                //O Bearer identifica recursos protegidos por um OAuth2.
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(response => response.json())
            .then(dados => {
                setListaTipoVaga(dados);
            })
            .catch(err => console.error(err));
    }

    const listarTipoCurso = () => {
        fetch('http://localhost:5000/empresa/tiposCurso', {
            method: 'GET',
            headers: {
                //Bearer authentication é o token authentication, um Schema para autenticação HTTP 
                //O Bearer identifica recursos protegidos por um OAuth2.
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(response => response.json())
            .then(dados => {
                setListaTipoCurso(dados);
            })
            .catch(err => console.error(err));
    }

    let history = useHistory()

    const salvar = () => {
        const form = {
            titulo: titulo,
            descricao: descricao,
            palavraChave: palavraChave,
            endereco: endereco,
            salario: salario,
            idTipoCurso: idTipoCurso,
            idTipoVaga: idTipoVaga

        }



        fetch('http://localhost:5000/empresa/cadastrarVaga', {
            method: 'post',
            headers: {
                'content-type': 'application/json',
                authorization: 'Bearer ' + localStorage.getItem('token')
            },
            body: JSON.stringify(form)
        })
            .then(re => re.json())
            .then(data => {
                if(data.msgsucesso !== undefined){
                    alert(data.msgsucesso)
                    history.push('/empresa/minhasVagas')
                }else if(data.msgerro !== undefined){
                    alert(data.msgerro)
                }
            })
            .catch(e => console.log(e))
    }
    return (
        <div className="main">
            <Header descricao="" />

            <div className="background">
                <section className="inputBg">
                    <form onSubmit={e => {
                        e.preventDefault()
                        salvar();
                    }} className="content-bg">
                        <h1 className="textVaga">Cadastrar Vaga</h1>
                        <div className="campo">
                            <label className="label2">Título</label>
                            <Input onChange={e => setTitulo(e.target.value)} className="tituloVaga" type="text" name="tituloVaga" label="" minLength={5} maxLength={40} />
                        </div>

                        <div className="campo">
                            <label className="label2">Salário</label>
                            <Input onChange={e => setSalario(e.target.value)} className="tituloSalario" type="number" name="tituloSalario" label="" minLength={3} maxLength={9} />
                        </div>
                        <div className="campo">
                            <label className="label2">Localização</label>
                            <Input onChange={e => setEndereco(e.target.value)} className="tituloLocalizacao" type="text" name="tituloLocalizacao" label="" minLength={5} maxLength={40} />
                        </div>


                        <div className="campo">
                            <select onChange={e => setIdTipoVaga(e.target.value)} className="tituloTipoVaga" id="tipoVaga" name="tipoVaga">
                                <option value="AreaAtuacao">Tipo de Vaga</option>
                                {
                                    listaTipoVaga.map((item: any) => {
                                        return <option value={item.idTipoVaga}>{item.nomeTipoVaga}</option>
                                    })
                                }
                            </select>
                        </div>
                        <div>
                            <select onChange={e => setIdTipoCurso(e.target.value)} className="tituloTipoCurso" placeholder="Tipo de Curso" id="tipoVaga" name="tipoCurso">
                                <option value="AreaAtuacao">Tipo de Curso</option>
                                {
                                    listaTipoCurso.map((item: any) => {
                                        return <option value={item.idTipoCurso}>{item.nomeTipoCurso}</option>
                                    })
                                }
                            </select>
                        </div>

                        <div className="campo">
                            <Input onChange={e => setPalavraChave(e.target.value)} className="palavrachave" type="text" placeholder="palavrachave" name="palavrachave" label="" />
                        </div>
                        <div className="campo">
                            <textarea onChange={e => setDescricao(e.target.value)} className="tituloDescricao" placeholder="Descrição" minLength={10} maxLength={500} ></textarea>
                        </div>

                        <Button value="Publicar" />

                    </form>
                </section>
            </div>

            <Footer />
        </div>
    )
}

export default CadastroVaga;