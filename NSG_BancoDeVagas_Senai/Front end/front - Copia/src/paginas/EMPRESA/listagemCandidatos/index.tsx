import React, { useEffect, useState } from 'react';
import {Link, useHistory} from 'react-router-dom';
import Header from '../../../componentes/header/index'
import Footer from '../../../componentes/footer/index'
import Input from '../../../componentes/input/index'
import './style.css'
import Button from '../../../componentes/button';
import ButtonContratar from '../../../componentes/buttonContratar';
import TdCol from '../../../componentes/tdCol/index';
import enterprise from '../../../assets/img/enterprise.png';
import Vaga from '../../../componentes/vagas';

function ListagemCandidatosVaga()
{
    const [candidaturas, setCandidaturas] = useState<any>([])
    const [vaga, setVaga] = useState<any>({})
    const [tipoVaga, setTipoVaga] = useState<any>({})




    var hist = useHistory()


    function curriculoPorId(id:''){
        hist.push('/curriculoDoUsuario/id?='+ parseInt(id))
    }

    function dataConverter(value:string){
        var data = new Date(value)
        
        var mes = data.getMonth().valueOf() + 1;

        return ' ' + data.getDate()+ '/' + mes + '/' + data.getFullYear() 
    }

    
    function desativarVaga (id:number){

        
        fetch('HTTP://localhost:5000/empresa/vagas/desativar/' + id ,{
            method:'PUT',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        })
        .then(() => {
            alert('A vaga ' + id + ' foi desativada com sucesso!')
            hist.push('/empresa/minhasVagas')
        }).catch(a => console.log(a))

    }


    const extrairUrl = () => {
        var url = window.location.href
        var idVaga = url.split('=')[1]
        return parseInt(idVaga)
    }
    
    useEffect(() => {
        infoVaga()
        buscarCandidatos()
    }, [])

    function escolherCandidato(escolher:number){
        fetch('http://localhost:5000/empresa/escolherCandidato/' + escolher, {
            method:'post',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
        .then(data => {
            alert(data.msgerro)
            alert(data.msgsucesso)
            hist.push('/empresa/meuperfil')
        })
    } 

    const infoVaga = () => {
        var id = extrairUrl()

        fetch('http://localhost:5000/empresa/vaga/'  + id, {
            method:'get',
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
        .then(data => {
            console.log(data)
            setVaga(data)
            setTipoVaga(data.idTipoVagaNavigation)
        }).catch(a => console.log(a))
    }


    const buscarCandidatos = () => {
        var id = extrairUrl()
        fetch('http://localhost:5000/empresa/vagas/'+ id, {
            headers:{
                "Content-Type": "application/json",
                authorization:'Bearer ' + localStorage.getItem('token')
            }
        }).then(resp => resp.json())
        .then(data => {
            setCandidaturas(data)
            console.log(data)
        }).catch(a => console.log(a))
    }

    return(
        <div className="telaListagemCandidatos">
            <Header descricao=""/>
            <section className="informacoesVaga">
                <div className="infor">
                    <p><strong>{vaga.titulo}</strong></p>
                    <p className="vagaTipo"><strong>{tipoVaga.nomeTipoVaga}</strong></p>
                    <p className="loc">{vaga.endereco}</p>
                </div>

                <div className="moreInfo">
                    <textarea disabled style={{color:"black", margin:"20px", width:"800px", height:"100px", resize:"none"}} value={vaga.descricao} > </textarea>
                </div>
                <button onClick={()=> desativarVaga(vaga.idVaga)} style={{cursor:"pointer",padding:"5px",width:"120px", height:"40px", textAlign:"center", background:"red", color:"white", borderRadius:"6px",border:"none", margin:"10px"}}> Desativar vaga </button>

            </section>
            


            <section className="tabelaListagem">
                <table>
                    <tr>
                        <td className="titleTable" colSpan={2}>Nome do Candidato</td>
                        <td className="titleTable2">data</td>
                        <td className="titleTable2">...</td>
                        <td className="titleTable2">...</td>


                    </tr>
            {candidaturas.map((item:any) => {
                return(
                    <tr>
                        <td className="nomeCandidato" colSpan={2}>{item.idCandidatoNavigation.nomeAluno}</td>
                        <td className="especialidadeCandidato">{dataConverter(item.dataCandidatura)}</td>
                        <td onClick={() => curriculoPorId(item.idCandidatoNavigation.idUsuario)} style={{cursor:"pointer", color:"red", marginRight:"15px"}}> Currículo  </td>
                        <td onClick={() => escolherCandidato(item.idCandidatura)} style={{cursor:"pointer", color:"blue"}} > Contratar </td>
                    </tr>
                )
            })}
                    
                </table>
            </section>
            <Footer />
        </div>
    )
}

export default ListagemCandidatosVaga;