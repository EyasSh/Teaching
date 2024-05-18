import React from 'react';
import './logo.css'
function Logo(props) {
    const flexDirection = props.flex === 'row' ? 'row' : 'column';
    return (
        <div className={`logo-container ${flexDirection}`}>
            <h2 className='brand'>ReCoursia</h2>
            <svg  class="rotating-arrow"  xmlns="http://www.w3.org/2000/svg" width="2em" height="2em" viewBox="0 0 512 512">
            <defs>
                <linearGradient id="grad1" x1="0%" y1="0%" x2="100%" y2="0%">
                    <stop offset="0%" stop-color="#66afe9" />
                    <stop offset="100%" stop-color="#804fff" />
                </linearGradient>
            </defs>
            <path fill="url(#grad1)"
            d="M447.1 86.2C400.3 33.4 332.2 0 256 0C114.6 0 0 114.6 0 256h64c0-106.1 85.9-192 192-192c58.5 0 110.4 26.5 145.5 67.8L341.3 192H512V21.3zM256 448c-58.5 0-110.4-26.5-145.5-67.8l60.2-60.2H0v170.7l64.9-64.9c46.8 52.8 115 86.2 191.1 86.2c141.4 0 256-114.6 256-256h-64c0 106.1-85.9 192-192 192">
                </path>
            </svg>
        </div>
    );
}

export default Logo;