import React, { useEffect } from 'react'
import "./Home.css"
import { Link } from 'react-router-dom'
import Button from '@mui/material/Button';

const Home = (props) => {

  return (
    <section>
      <div className='title'>Welcome to Los Pollos Hermanos Hospital!</div>
      <Link to="/login">
        <Button variant="contained" color="success" sx={{position: "relative", margin: "20px"}} onClick={() => { props.setUser("doctor") }}>Doctor</Button>
      </Link>
      <Link to="/patient/home">
        <Button variant="contained" color="success" sx={{position: "relative"}} onClick={() => { props.setUser("patient") }}>Patient</Button>
      </Link>
      
    </section>

  )
}

export default Home