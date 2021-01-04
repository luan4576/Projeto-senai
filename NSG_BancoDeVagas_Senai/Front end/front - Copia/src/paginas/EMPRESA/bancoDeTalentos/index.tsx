import React, { useState } from 'react'
import Header from '../../../componentes/header'
import './style.css'
import imgUsr from '../../../assets/img/user.jpg'
import Footer from '../../../componentes/footer'
import { useHistory } from 'react-router-dom'
import logo from '../../../assets/img/LogoTipografia.png'



function BancoDeTalentos(){

    const [pesquisa, setPesquisa] = useState('')
    const [curriculoUser, setCurriculo] = useState([])

    let hist = useHistory()

    function curriculoPorId(id:''){
        hist.push('/curriculoDoUsuario/id?='+ parseInt(id))
    }

    function trazerImg(imagem:string){
        if(imagem === undefined || imagem === ''){
            return imgUsr
        }   
        return 'http://localhost:5000/imagens/' + imagem
    }

    const buscarCurriculo = () => {
        fetch('http://localhost:5000/bancoDeTalentos/'+ pesquisa, {
            method:'get',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
        .then(dados => {
            setCurriculo(dados)
        }).catch(e => console.log(e))
    }

    return (
        <div>
            <Header descricao="" />
            <section id="pesquisa-nv1">
            <img src={logo} />

                <div id="pesquisa-nv2">
                    <h3>Pesquise um talento</h3>
                    <form onSubmit={eve => {eve.preventDefault(); buscarCurriculo()}}>
                        <input onChange={eve => setPesquisa(eve.target.value)} type="text" placeholder="Pesquise alguma característica pelo nosso banco de talentos.." />
                    </form>

                </div>
            </section>

            <section id="conteudo-nv1" style={{marginBottom:"330px"}}>
                {curriculoUser.map((item:any) => {
                    return (
                        <div id="conteudo-nv2" onClick={() => {curriculoPorId(item.idCandidatoNavigation.idUsuario)}} >
                            <img src={trazerImg(item.idCandidatoNavigation.foto)} />
                            <p>{item.idCandidatoNavigation.nomeAluno}</p>
                            <p>{item.palavraChave}</p>
                        </div>
                    )
                })}
            </section>

            <Footer />
        </div>
    )

}

export default BancoDeTalentos