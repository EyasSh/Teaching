import React, { useState } from 'react';
import './signup.css'
import Logo from '../Logo/Logo';
import Input from '../Inputs/Input';
import Button from '../Button/Button';
import UserHandler from '../../UserHandler/userHandler';


function Signup(props) {
    const [name,setName] =useState("")
    const [email,setEmail] =useState("")
    const [password, setPassword] =useState("")
    const [dob,setDob] = useState(null)
    let handleSup = async ()=>{
        try{
            console.log(`Signup called with name: ${name}, email: ${email}, password: ${password}, dob: ${dob}`);
            let res = await UserHandler.Signup(name,email,password,dob);
        }
        catch(e){
            alert(e.Message)
        }
    }
    return (
        <div className='container'>
            <Logo flex="column"/>
           
            <Input type='text' placeholder='Full Name' action={setName}  />
           
            <Input type='email' placeholder='Email'  action={setEmail}  />
            
            <Input type='password' placeholder='Password'  action={setPassword}  />
            
            <Input type='date' placeholder='dd/mm/yyyy' action={setDob} />
            <Button  status='signup' content="Sign Up" action={handleSup} /> 
            <span className='warning-message'>{}</span>
        </div>
    );
}

export default Signup;