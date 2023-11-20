import React, { useState } from 'react';
import './Registration.css'; // Import the CSS file
import { useNavigate } from 'react-router-dom';

const Registration = (props) => {

  const navigate = useNavigate()

  const [formData, setFormData] = useState({
    username: '',
    password: '',
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    medicalNumber: '',
    role: '',
    ward: ''
  });

  const [errors, setErrors] = useState({});

  const validateEmail = (email) => {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  };

  const validatePhoneNumber = (phoneNumber) => {
    return /^\+\d{11}$/.test(phoneNumber);
  };

  const validateMedicalNumber = (medicalNumber) => {
    return /^\d{3}-\d{3}-\d{3}$/.test(medicalNumber);
  };

  const registerFetch = (formData) => {
    return fetch(`api/Auth/Register`,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(formData),
        }).then(res => res.json())
}

  const handleSubmit = (e) => {
    e.preventDefault();
    const newErrors = {};
    let hasError = false;
    if ( props.cookie["role"] === "Admin") {
      formData.role = "Doctor"
      formData.medicalNumber = "a"
     
    }
    else{
      formData.role = "Patient"
      formData.ward = "a"
      if (!validateMedicalNumber(formData.medicalNumber)) {
        newErrors.medicalNumber = 'Invalid medical number format';
        hasError = true;
      }
    }

    

    if ( props.cookie["role"] === "Admin") {
      formData.role = "Doctor"
      formData.medicalNumber = "a"
    }
    else{
      formData.role = "Patient"
      formData.ward = "a"
      
      if (!validateMedicalNumber(formData.medicalNumber)) {
        newErrors.medicalNumber = 'Invalid medical number format';
        hasError = true;
      }
    }

    for (const field in formData) {
      if (!formData[field]) {
        newErrors[field] = 'This field is required';
        hasError = true;
      }
    }
    console.log(hasError)
    if (!validateEmail(formData.email)) {
      newErrors.email = 'Invalid email format';
      hasError = true;
    }

    if (!validatePhoneNumber(formData.phoneNumber)) {
      newErrors.phoneNumber = 'Invalid phone number format';
      hasError = true;
    }

  
    if (formData.password.length < 6) {
        newErrors.password = 'Password must be at least 6 characters long';
        hasError = true;
    }

    if (hasError) {
      console.log("error")
      setErrors(newErrors);

    } else {

      

      console.log('Form submitted with data:', formData);
      /*
      setFormData({
        username: '',
        password: '',
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        medicalNumber: '',
        role: '',
        ward: ''
      });
      */
      setErrors({});
      
      registerFetch(formData).then(res => {
        console.log(res)
        if(props.cookie["role"] === "Admin"){
          navigate("/adminHome")
        }else{
          navigate("/")
        }
        
    })
    }    
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleGoBack = () => {
    if(props.cookie["role"] === "Admin"){
      console.log("asdertre")
      navigate("/adminHome")
    }else{
      navigate("/")
    }
  }

  return (
    <div className="container">
      <h2 className="form-heading">Patient Registration</h2>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label className="form-label">Username:</label>
          <input
            type="text"
            name="username"
            value={formData.username}
            onChange={handleChange}
            className="form-input"
            required
          />
          {errors.username && <p className="form-error">{errors.username}</p>}
        </div>

        <div className="form-group">
          <label className="form-label">Password:</label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            className="form-input"
            required
          />
          {errors.password && <p className="form-error">{errors.password}</p>}
        </div>

        <div className="form-group">
          <label className="form-label">First Name:</label>
          <input
            type="text"
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
            className="form-input"
            required
          />
          {errors.firstName && <p className="form-error">{errors.firstName}</p>}
        </div>

        <div className="form-group">
          <label className="form-label">Last Name:</label>
          <input
            type="text"
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
            className="form-input"
            required
          />
          {errors.lastName && <p className="form-error">{errors.lastName}</p>}
        </div>

        <div className="form-group">
          <label className="form-label">Email:</label>
          <input
            type="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            className="form-input"
            required
          />
          {errors.email && <p className="form-error">{errors.email}</p>}
        </div>

        <div className="form-group">
          <label className="form-label">Phone Number:</label>
          <input
            type="text"
            name="phoneNumber"
            value={formData.phoneNumber}
            onChange={handleChange}
            className="form-input"
            required
          />
          {errors.phoneNumber && <p className="form-error">{errors.phoneNumber}</p>}
        </div>

        {props.cookie["role"] === "Admin" ? [

          <div className="form-group">
            <label className="form-label">Ward:</label>
            <input
              type="text"
              name="ward"
              value={formData.ward}
              onChange={handleChange}
              className="form-input"
              required
            />
            {errors.ward && <p className="form-error">{errors.ward}</p>}
          </div>

        ] : [
          <div className="form-group">
            <label className="form-label">Medical Number:</label>
            <input
              type="text"
              name="medicalNumber"
              value={formData.medicalNumber}
              onChange={handleChange}
              className="form-input"
              required
            />
            {errors.medicalNumber && <p className="form-error">{errors.medicalNumber}</p>}
          </div>
        ]}

        <button onClick={() => (handleGoBack())} className="form-goBack">Go Back</button>
        <button type="submit" className="form-submit">Register</button>
      </form>
    </div>
  );
};

export default Registration;