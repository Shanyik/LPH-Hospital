import React from 'react'
import "./WelcomeScreen.css"
import { Button } from "@mui/material";
import { Link } from 'react-router-dom';

const WelcomeScreen = () => {
  return (
    <section>
      <div className="background-image">
        <div className='info'> 
          <div>
            <img className='logo' src={require('../../Images/lphh-logo2.png')}></img>
            <div>
              <br/>
              <Link to="/login">
                <Button style={{margin: 10}} className='welcomeButton' variant="contained" size="large" color='success'>Login</Button>
              </Link>
              <Link to="/registration">
                <Button className='welcomeButton' variant="contained" size="large" color='success'>Registration</Button>
              </Link>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}

export default WelcomeScreen