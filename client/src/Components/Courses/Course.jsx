import React, { useState, useEffect } from 'react';
import "./courses.css";
import Button from '../Button/Button';
import fallbackImage from '../../assets/Images/Fail.jpg'; // Ensure the path is correct

function CourseCard(props) {
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
      <div className='buttons'>
      <Button
        className="but"
        content="Preview Course"
        link="http://localhost:3000/signup"
        status='action'
        />
        <Button
        className="but"
        content="Purchase Course"
        link="http://localhost:3000/signup"
        status='purchase'
         />

      </div>
    </div>
  );
}

export default CourseCard;
