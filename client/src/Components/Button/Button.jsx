import React from 'react';
import './button.css';

function Button(props) {
    const statusClass = statusHandler(props.status);
    
    return (
        <>
            {statusClass !== '' ? (
                <button className={statusClass}>
                    {props.content.toString()}
                </button>
            ) : (
                <button className="default-class">
                    {props.content.toString()}
                </button>
            )}
        </>
    );
}

function statusHandler(status) {
    switch (status) {
        case "action":
            return "action";
        case "purchase":
            return "purchase";
        case "signup":
            return "signup";
        default:
            return "";
    }
}

export default Button;
