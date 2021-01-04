import React, { useEffect, useState } from 'react';
import Header from '../../../componentes/header';
import Footer from '../../../componentes/footer/index';
import Button from '../../../componentes/button/index';
import vaga from '../../../assets/img/team.png';
import curriculo from '../../../assets/img/paper.png';
import engrenagem from '../../../assets/img/engr.png';
import user from '../../../assets/img/enterprise.png'
import './style.css';
import { useHistory } from 'react-router-dom';

function PerfilEmpresa() {

    useEffect(() => {
        ChamarEmpresa()
    }, [])

    let hist = useHistory()

    const [empresa, setEmpresa] = useState<any>({})
    let [imagem, setImg] = useState('')


    const ChamarEmpresa = () => {

        fetch('http://localhost:5000/Empresa/Perfil', {
            method: 'get',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem("token")
            }
        })
            .then(resp => resp.json())
            .then(dados => {
                setEmpresa(dados)
                var imagem = dados.imagemEmpresa
                if (imagem === null || imagem === undefined || imagem === '') {
                    setImg(user)
                } else {
                    setImg('http://localhost:5000/imagens/' + imagem)
                }
            })
            .catch(erro => console.log(erro))
    }


    return (
        <div>
            <Header descricao="" />
            <div className="main1">
                <section className="usuario">
                    <div className="infoUsuario">
                        <img className="iconGg" src={imagem} />
                        <div className="texto">
                            <h1>{empresa.nomeFantasia}</h1>
                        </div>

                    </div>
                    <div className="resumo" >
                        <h3>informaçao:</h3>
                        <p>{empresa.razaoSocial}</p>
                        <h4>CNPJ:</h4>
                        <p>{empresa.cnpj}</p>
                        <h5>CNAE:</h5>
                        <p>{empresa.cnae}</p>
                        <p onClick={() => hist.push('/empresa/atualizar')} style={{ marginTop: "10px", color: "blue", cursor: "pointer" }}>Editar perfil</p>

                    </div>
                </section>


                <section className="botoes1">
                    <div className="fileira">
                        <div className="opcoess">
                            <div className="img">
                                <img style={{cursor:"pointer"}} src={curriculo} onClick={() => hist.push('/bancoDeTalentos')} className="icon" />
                            </div>
                            <p>Banco de talentos</p>
                        </div>
                        <div className="">
                            <div className="img">
                                <img style={{cursor:"pointer"}} src={vaga} onClick={() => hist.push('/empresa/cadastroVaga')} className="icon" />
                            </div>
                            <p>Cadastrar vaga</p>
                        </div>
                    </div>
                    <div className="fileira">
                        <div className="opcoess">
                            <div className="img">
                                <img style={{cursor:"pointer"}} src={engrenagem} onClick={() => hist.push('/empresa/minhasVagas')} className="icon" />
                            </div>
                            <p>Vagas já cadastradas</p>
                        </div>
                    </div>

                </section>
            </div>



            <Footer />
        </div>
    )
}

export default PerfilEmpresa;