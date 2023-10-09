import React from 'react'
import "./Home.css"

const Home = (props) => {



  return (
    <section>
      <div className='title'>Welcome to Los Pollos Hermlanos Hospital!</div>
      <button onClick={() => {props.setUser("doctor")}}>Doctor</button>
      <button onClick={() => {props.setUser("patient")}}>Patient</button>
      <button onClick={() => {props.setUser(null)}}>logout</button>
    </section>
    
  )
}

export default Home