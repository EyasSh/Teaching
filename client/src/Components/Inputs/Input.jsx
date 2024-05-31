import React from 'react';
import './inputs.css'

function Input(props) {
    const validInputTypes = [
        "text", "password", "email", "number", "search", "tel", "url", "date", 
        "datetime-local", "month", "week", "time", "color", "checkbox", "radio", 
        "file", "range", "button", "reset", "submit", "image", "hidden"
    ];
    const handleChange=(e) => {
        props.action(e.target.value);
        
    }
    const { type, placeholder } = props;
    const inputType = validInputTypes.includes(type) ? type : 'text';

    return (
        <>
            <input type={inputType} placeholder={placeholder} onChange={handleChange}  />
        </>
    );
}

export default Input;
