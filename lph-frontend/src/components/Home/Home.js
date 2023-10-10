import React, { useEffect } from 'react'
import "./Home.css"
import { Link } from 'react-router-dom'

const Home = (props) => {



  return (
    <section>
      <div className='title'>Welcome to Los Pollos Hermlanos Hospital!</div>
      <Link to="/main">
        <button onClick={() => { props.setUser("doctor") }}>Doctor</button>
      </Link>
      <button onClick={() => { props.setUser("patient") }}>Patient</button>
      
    </section>

  )
}

export default Home