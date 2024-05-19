import React from 'react';
import './signup.css'
import Logo from '../Logo/Logo';
import Input from '../Inputs/Input';
import Button from '../Button/Button';

function Signup(props) {
    return (
        <div className='container'>
            <Logo flex="column"/>
           
            <Input type='text' placeholder='Full Name'  />
           
            <Input type='email' placeholder='Email'  />
            
            <Input type='password' placeholder='Password'  />
            
            <Input type='date' placeholder='dd/mm/yyyy' />
            <Button  status='signup' content="Sign Up" action={()=>alert("Signup")} /> 
        </div>
    );
}

export default Signup;