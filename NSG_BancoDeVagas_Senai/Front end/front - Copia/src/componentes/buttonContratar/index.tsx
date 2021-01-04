import React from 'react';
import './style.css';

interface ButtonProps{
    value: string;
}

const ButtonContratar:React.FC<ButtonProps>= ({value})=>{
    return(
        <div>
            <input className="buttonContratar" type="submit" value={value}/>
        </div>
    );
}

export default ButtonContratar;