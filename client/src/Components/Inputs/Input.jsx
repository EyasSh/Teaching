import React from 'react';
import './inputs.css'

function Input(props) {
    const validInputTypes = [
        "text", "password", "email", "number", "search", "tel", "url", "date", 
        "datetime-local", "month", "week", "time", "color", "checkbox", "radio", 
        "file", "range", "button", "reset", "submit", "image", "hidden"
    ];
    const handleChange=(e) => {
        if(props.type=='date'){
            const dateString = e.target.value;
            const dateParts = dateString.split('-');
            const year = parseInt(dateParts[0]);
            const month = parseInt(dateParts[1]) - 1; // Months are 0-based in JavaScript
            const day = parseInt(dateParts[2]);
            const newDate = new Date(year, month, day);
            props.action(newDate)
            return
        }
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
