import React from 'react';
import './style.css'

interface VagaProps{
    texto:any
    padd:string
}

const Vaga:React.FC<VagaProps> = (props) => {
    return(
        <div className="bg">
            <ul style={{listStyle:"none"}}>
                <li style={{padding:props.padd}}>{props.texto}</li>
            </ul>
        </div>
    )
}

export default Vaga