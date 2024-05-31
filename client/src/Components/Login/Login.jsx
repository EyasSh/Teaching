import React, { useState } from 'react';
import './Login.css'
import Logo from '../Logo/Logo';
import Input from '../Inputs/Input';
import Button from '../Button/Button'

function Login(props) {
    const [email,setEmail] = useState('');
    const [password,setPassword]=useState('');
    const handleForm= ()=>{
        
    }
    return (
        <div className='container'>
           <Logo  flex='column'/>
           <Input type='email' placeholder='Email'action={setEmail}></Input>
           <Input type='password' placeholder='Password' action={setPassword}></Input>
           <Button status='action' content='Login' action={()=>alert(`${email}\n${password}`)}  />
           <div className='sup'>
            <span>Don't have an account?</span>
            <Button link='/signup' status='signup' content="Sign Up"  /> 
           </div>
            
        </div>
    );
}

export default Login;