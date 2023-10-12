import React, { useEffect } from 'react'
import "./Home.css"
import { Link } from 'react-router-dom'

const Home = (props) => {

  return (
    <section>
      <div className='title'>Welcome to Los Pollos Hermanos Hospital!</div>
      <Link to="/main">
        <button onClick={() => { props.setUser("doctor") }}>Doctor</button>
      </Link>
      <Link to="/patient/home">
        <button onClick={() => { props.setUser("patient") }}>Patient</button>
      </Link>
      
    </section>

  )
}

export default Home