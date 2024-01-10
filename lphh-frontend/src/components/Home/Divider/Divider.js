import React from 'react'
import "./Divider.css"

export const Divider = (props) => {
  return (
    <section className="divider">
      <div >
        <p className='name'>{props.name}</p>
      </div>
    </section>
    
  )
}