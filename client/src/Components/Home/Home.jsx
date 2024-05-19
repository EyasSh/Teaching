import React from 'react';
import './home.css'
import Logo from '../Logo/Logo';

function Home(props) {
    return (
        <>
           <nav className='bar'>
            <Logo flex='row' />
            <span>
                Hello Profile owner name
            </span>
           </nav>
        </>
    );
}

export default Home;