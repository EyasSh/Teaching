import React, { useState } from 'react';
import './signup.css'
import Logo from '../Logo/Logo';
import Input from '../Inputs/Input';
import Button from '../Button/Button';

function Signup(props) {
    const [name,setName] =useState("")
    const [email,setEmail] =useState("")
    const [password, setPassword] =useState("")
    const [dob,setDob] = useState(null)
    return (
        <div className='container'>
            <Logo flex="column"/>
           
            <Input type='text' placeholder='Full Name' action={setName}  />
           
            <Input type='email' placeholder='Email'  action={setEmail}  />
            
            <Input type='password' placeholder='Password'  action={setPassword}  />
            
            <Input type='date' placeholder='dd/mm/yyyy' action={setDob} />
            <Button  status='signup' content="Sign Up" action={()=>alert(dob)} /> 
            <span className='warning-message'>{}</span>
        </div>
    );
}

export default Signup;