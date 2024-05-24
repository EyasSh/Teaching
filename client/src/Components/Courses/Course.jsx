import React from 'react';
import "./courses.css"
import Button from '../Button/Button'
function CourseCard(props) {
    return (
        <div className='cont'>
          
                <img className='responsive-image' 
                src={props.image} alt=''
                />
           
                <h3>Xamarin</h3>
                <section>An extensive MAUI Course in C#</section>
                <Button className="but" content="View Corse" 
                link="http://localhost:3000/signup"
                status='action' 
                />
            
            
        </div>
    );
}

export default CourseCard;