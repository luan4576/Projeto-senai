import React, { useEffect, useState } from 'react';
import Vaga from '../../../componentes/vagas'
import user from '../../../assets/img/enterprise.png'
import Header from '../../../componentes/header';
import Footer from '../../../componentes/footer';
import './style.css'
import { useHistory } from 'react-router-dom';

function PerfilDaEmpresa() {

    const [empresa, setEmpresa] = useState<any>({})
    const [vaga, setVaga] = useState<any>({})
    const [tipoVaga, setTipoVaga] = useState<any>({})
    const [candidato, setCandidato] = useState<any>({})

    let [imagem, setImg] = useState('')

    const [prazo, setPrazo] = useState([])

    useEffect(() => {
        buscarEstagio()
    }, [])

    var hist = useHistory()

    const extrairUrl = () => {
        var url = window.location.href
        var idVaga = url.split('=')[1]
        return parseInt(idVaga)
    }

    function dataConverter(value:Date){
        var data = new Date(value)
        
        var mes = data.getMonth().valueOf() + 1;

        return ' ' + data.getDate()+ '/' + mes + '/' + data.getFullYear() 
    }

    const buscarEstagio = () => {
        fetch('http://localhost:5000/administrador/estagiosComPrazos/'+ extrairUrl(), {
            method:'get',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        })
        .then(re => re.json())
        .then(data => {

            var imagem = data.idVagaNavigation.idEmpresaNavigation.imagemEmpresa
            if (imagem === null || imagem === undefined || imagem === '') {
                setImg(user)
            } else {
                setImg('http://localhost:5000/imagens/' + imagem)
            }

            if(data.msgerro !== undefined){
                alert(data.msgerro)
                hist.push('/administrador/listagemEstagios')
            }else{

                setEmpresa(data.idVagaNavigation.idEmpresaNavigation)
                setTipoVaga(data.idTipoVagaNavigation)
                setVaga(data.idVagaNavigation)
                setCandidato(data.idCandidatoNavigation)
                console.log(data.idVagaNavigation)

                if(data.idVagaNavigation.prazos !== undefined){
                    setPrazo(data.idVagaNavigation.prazos)
                }
                
            }

        })
    }

    return (
        <div>
            <Header descricao="" />
            <div className="content-empresa">
                <section className="empresa">
                    <div className="infoUsuario">
                        <img className="iconEmpresa" src={imagem} />
                        <div className="texto">
                            <h1>{empresa.nomeFantasia}</h1>
                        </div>
                    </div>
                </section>
                <section className="infoEmpresa">
                    <div className="resumoEmpresa">
                        <h3 className="titleRes">{vaga.titulo}</h3>
                        <h3 className="titleRes">{  'd'}</h3>

                        <p>{vaga.descricao}</p>
                    </div>
                    <div className="vagasAbertas">
                        <h3> {candidato.nomeAluno} </h3>
                        <h3 style={{fontSize:"10px"}}>Prazo de início -- Prazo final (cada prazo vale 3 meses)</h3>

                        {prazo.map((item:any) => {
                        return ( 
                            <Vaga padd="10px" texto={ dataConverter(item.prazoInicio) + ' ---' + dataConverter(item.prazoFim) } />
                            )
                        })} 

                    </div>
                </section>
            </div>

            <Footer />
        </div>
    )
}

export default PerfilDaEmpresa;