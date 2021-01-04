import React from 'react';
import './style.css';

interface ButtonProps{
    value: string;
}

const Button:React.FC<ButtonProps>= ({value})=>{
    return(
        <div>
            <input className="button" type="submit" style={{cursor:"pointer", border:"none"}} value={value}/>
        </div>
    );
}

export default Button;