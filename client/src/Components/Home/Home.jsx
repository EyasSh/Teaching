import React from 'react';
import './home.css'
import Logo from '../Logo/Logo';
import CourseCard from '../Courses/Course';
import IMG from "../../assets/Images/Fail.jpg"
import AvailableCorses from '../AvailableCourses/AvailableCourses'

function Home(props) {
    return (
        <div className='home'>
           <nav className='bar'>
            <Logo flex='row' />
            <span>
                Hello Profile owner name
            </span>
           </nav>
           <h2 className='header'>Owned Courses:</h2>
           <div className='grid'>
                <AvailableCorses image={IMG} />
                <AvailableCorses image={IMG} />
                <AvailableCorses image={IMG} />
                <AvailableCorses image={IMG} />
                <AvailableCorses image={IMG} />
                <AvailableCorses image={IMG} />
           </div>
           <h2 className='header'>Suggested Courses:</h2>
           <div className='grid'>
                <CourseCard image={IMG}></CourseCard>
                <CourseCard image={IMG}></CourseCard>
                <CourseCard image={IMG}></CourseCard>
                <CourseCard image={IMG}></CourseCard>
                <CourseCard image={IMG}></CourseCard>
                <CourseCard image={IMG}></CourseCard>
           </div>
           
        </div>
    );
}

export default Home;