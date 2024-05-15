import React from 'react';
import './Login.css'

function Login(props) {
    return (
        <div className='login-container'>
            <div className='login-wrapper'>
                <label>Email</label>
                <input type='text' />
                <label>Password</label>
                <input type='password' />
                <input type='submit' />
            </div>
            
        </div>
    );
}

export default Login;