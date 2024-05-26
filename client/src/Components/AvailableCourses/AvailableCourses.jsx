import React,{useEffect, useState} from 'react';
import '../Courses/courses.css'
import './available.css'
import Button from '../Button/Button';
import fallbackImage from '../../assets/Images/Fail.jpg'; // Ensure the path is correct
function Purchased(props) {
    const [imageSrc, setImageSrc] = useState(props.image);

    useEffect(() => {
      const fetchImage = async () => {
        try {
          const response = await fetch(props.image);
          if (response.ok) {
            setImageSrc(props.image);
          } else {
            setImageSrc(fallbackImage);
          }
        } catch (error) {
          setImageSrc(fallbackImage);
        }
      };
  
      fetchImage();
    }, [props.image]);
  
    const handleImageError = () => {
      setImageSrc(fallbackImage);
    };
    return (
        <div className='cont'>
      <img
        className='responsive-image'
        src={imageSrc}
        alt='Course'
        onError={handleImageError}
      />
      <h3>Xamarin</h3>
      <section>An extensive MAUI Course in C#</section>
      <div className='button'>
        <Button
          content="View Course"
          link="http://localhost:3000/signup"
          status='action'
        />
      </div>
      
      
      
    </div>
    );
}

export default Purchased;