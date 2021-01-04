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

function ListagemEstagios()
{

const [listar,setListar] = useState([])
const [vaga, setVaga] = useState<any>({})
const [prazos, setPrazos] = useState<any>({})

useEffect(()=>
    listarCandidato()
    ,[]
)

var hist = useHistory()

function estagioPorId(id:number){
    hist.push('/administrador/estagioPorId/id?='+ id)
}

const  listarCandidato = () =>{
        
    fetch('http://localhost:5000/administrador/EstagiosComPrazos', {
        method:'GET',
        headers:{
            "Content-Type": "application/json",
            authorization:'Bearer ' + localStorage.getItem('token')
        }
    }).then(resp => resp.json())
    .then(data => {
       setListar(data)
    //    setPrazos(data.idVagaNavigation.prazos)
       console.log(data)
       
    })
} 


function prorogarPrazo (id:number){

    
    fetch('http://localhost:5000/administrador/prorrogarEstagio/' + id ,{
        method:'POST',
        headers:{
            "Content-Type": "application/json",
            authorization:'Bearer ' + localStorage.getItem('token')
        }
    }).then(re => re.json())
    .then(data => {
        if(data.msgsucesso !== undefined){
            alert(data.msgsucesso)
        }else if(data.msgerro !== undefined){
            alert(data.msgerro)
        }
        
        
    })

}

    return(
        <div className="telaListagemEstagios">
            <Header descricao=""/>
            <h1 className="textMyVagas">Estágios ativos</h1>

            {listar.map((item:any)=>{
                return(
                    <div style={{margin:"10px", marginBottom:"50px"}}>
                        <section className="informacoesEstagios">
                        <p className="prazo">Contratante: {item.idVagaNavigation.idEmpresaNavigation.razaoSocial}</p>

                            <div className="infor">
                            </div>

                            <div className="moreInfo">
                                <p>{item.idVagaNavigation.titulo}</p>
                            </div>

                            <div className="desc">
                                <p> {item.idCandidatoNavigation.nomeAluno} </p>
                            </div>

                            
                            <button type="submit" onClick={()=> prorogarPrazo(item.idCandidatura)} style={{cursor:"pointer",width:"200px", height:"40px", textAlign:"center", background:"red", color:"white", borderRadius:"6px",border:"none"}}>
                                    Prorrogar prazo
                            </button>
                            <button onClick={() => estagioPorId(item.idCandidatura)} style={{cursor:"pointer", marginLeft:"10px", width:"200px", height:"40px", textAlign:"center", background:"red", color:"white", borderRadius:"6px",border:"none"}}>
                                    Detalhes
                            </button>
                
                    </section>

                    </div>
                )
            })}

            <Footer/>
        </div>
    )
}

export default ListagemEstagios;