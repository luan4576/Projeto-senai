import React, { useEffect, useState } from 'react';
import Header from '../../../componentes/header';
import Footer from '../../../componentes/footer/index';
import Button from '../../../componentes/button/index';
import vaga from '../../../assets/img/pcIcon.png';
import curriculo from '../../../assets/img/paper.png';
import mala from '../../../assets/img/work.png';
import engrenagem from '../../../assets/img/engr.png';
import user from '../../../assets/img/user.jpg'
import './style.css';
import { useHistory } from 'react-router-dom'
import EditarCurriculo from '../editarCurriculo';


function PerfilUsuario() {

    const [candidato, setCandidato] = useState<any>({})
    const [navigation, setNav] = useState<any>({})
    let [imagem, setImg] = useState('')

    let history = useHistory();

    useEffect(() => {
        chamarUsr()
    }, [])


    function dataConverter(value:Date){
        var data = new Date(value)
        
        var mes = data.getMonth().valueOf() + 1;

        return ' ' + data.getDate()+ '/' + mes + '/' + data.getFullYear() 
    }


    const chamarUsr = () => {

        fetch('http://localhost:5000/candidato/curriculo', {
            method: 'get',
            headers: {
                "Content-Type": "application/json",
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
            .then(dados => {
                let dt = dados.idCandidatoNavigation
                setCandidato(dados)
                setNav(dt)

                if (dt.foto === null || dt.foto === undefined || dt.foto === '') {
                    setImg(user)
                } else {
                    setImg('http://localhost:5000/imagens/' + dt.foto)
                }

            })
            .catch(err => console.error(err))


    }

    return (
        <div>
            <Header descricao="" />


            <div className="main1">
                <section className="candidato">
                    <div className="infoCandidato">

                        <img className="iconGg" src={imagem} />
                        <div className="texto">
                            <h1>{navigation.nomeAluno}</h1>
                        </div>

                    </div>
                    <div className="resumo" style={{ padding: "12px" }}>
                        <h3>Palavras chave:</h3>
                        <p>{candidato.palavraChave}</p>

                        <p><strong>Data de Nascimento:</strong>{dataConverter(navigation.dataNascimento)}</p>
                        <p><strong>CPF:</strong> {navigation.cpf}</p>
                    </div>
                </section>

                <section className="botoesCandidato" >
                    <div className="fileira">
                        <div className="opcoess" >
                            <div className="img">
                                <img src={vaga} style={{ cursor: "pointer" }} onClick={() => history.push('/home')} className="icon" />
                            </div>
                            <Button value="Ver vagas" />
                        </div>
                        <div className="">
                            <div className="img">
                                <img src={curriculo} style={{ cursor: "pointer" }} onClick={() => history.push('/editarCurriculo')} className="icon" />
                            </div>
                            <Button value="Currículo" />
                        </div>
                    </div>
                    <div className="fileira">
                        <div className="opcoess">
                            <div className="img">
                                <img src={mala} style={{ cursor: "pointer" }} onClick={() => { history.push("/minhasCandidaturas") }} className="iconG" />
                            </div>
                            <Button value="Candidaturas" />
                        </div>
                        <div className="">
                            <div className="img">
                                <img src={engrenagem} style={{ cursor: "pointer" }} onClick={() => { history.push("/editarInformacoes") }} className="icon" />
                            </div>
                            <Button value="Editar " />
                        </div>
                    </div>


                </section>
            </div>




            <Footer />
        </div>
    )
}

export default PerfilUsuario;