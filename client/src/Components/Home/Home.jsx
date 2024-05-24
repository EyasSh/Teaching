import React from 'react';
import './home.css'
import Logo from '../Logo/Logo';
import CourseCard from '../Courses/Course';
import IMG from "../../assets/Images/Fail.jpg"

function Home(props) {
    return (
        <>
           <nav className='bar'>
            <Logo flex='row' />
            <span>
                Hello Profile owner name
            </span>
           </nav>
           <CourseCard image={IMG}></CourseCard>
        </>
    );
}

export default Home;