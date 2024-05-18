import React from 'react';
import './Login.css'
import Logo from '../Logo/Logo';
import Input from '../Inputs/Input';
import Button from '../Button/Button'

function Login(props) {
    return (
        <div className='container'>
           <Logo  flex='column'/>
           <Input type='email' placeholder='Email'></Input>
           <Input type='password' placeholder='Password'></Input>
           <Button status='action' content='Login'  />
            
        </div>
    );
}

export default Login;