import React from 'react'
import "./Home.css"
import { Link } from 'react-router-dom'
import logo from '../Images/lphh-logo2.png';

const Home = () => {

  return (
    <div className="home-container">
    <div className="home-title">
      <img src={logo} alt="Logo" className="home-logo" />
      Welcome to Los Pollos Hermanos Hospital!
    </div>
    <div className="home-button-container">
      <Link to="/login" className="home-button">
        Login
      </Link>
      <Link to="/registration" className="home-button">
        Registration
      </Link>
    </div>
  </div>

  )
}

export default Home