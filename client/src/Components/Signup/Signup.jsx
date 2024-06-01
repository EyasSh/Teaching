import React, { useState } from 'react';
import './signup.css'
import Logo from '../Logo/Logo';
import Input from '../Inputs/Input';
import Button from '../Button/Button';

function Signup(props) {
    const [dob,setDob] = useState(null)
    return (
        <div className='container'>
            <Logo flex="column"/>
           
            <Input type='text' placeholder='Full Name'  />
           
            <Input type='email' placeholder='Email'  />
            
            <Input type='password' placeholder='Password'  />
            
            <Input type='date' placeholder='dd/mm/yyyy' action={setDob} />
            <Button  status='signup' content="Sign Up" action={()=>alert(dob)} /> 
        </div>
    );
}

export default Signup;