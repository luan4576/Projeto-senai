import React from 'react';
import './style.css';
import '../../assets/style/global.css';

function Footer()
{
    return(
        <section className="foooter">
            <article>
                <p>SENAI de informática</p>
                <p>Santa Cecília - SP</p>
            </article>

            <article>
                <hr/>
            </article>

            <article>
                <p>Telefone: 1234567890</p>
                <p>Email: senai@senai.com</p>
            </article>
        </section>
    )
}

export default Footer;