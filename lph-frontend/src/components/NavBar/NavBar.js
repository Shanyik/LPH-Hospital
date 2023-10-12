import React from 'react';
import "./NavBar.css"
import { Box, Grid } from "@mui/material";
import { Link } from 'react-router-dom'

const Navbar = (props) => {
  return (
    props.user === "patient" ? [
      <section>
       <Box className="navbar">
          <Grid container alignItems="center">
            
            <Grid item xs={2.2} >
                <Link className='menuButton' to="/">Home</Link>
            </Grid>
            <Grid item xs={2.2} >
                <Link className='menuButton' to="/patient/prescriptions">Prescriptions</Link>
            </Grid>
            <Grid item xs={2.2} >
                <Link className='menuButton' to="/patient/documents">Documents</Link>
            </Grid>
            <Grid item xs={1} >
            </Grid>
            <Grid item xs={2.2} >
                <Link className='menuButton' to="/profile"> Profile</Link>
            </Grid>
            <Grid item xs={2.2} >
                <Link className='menuButton' to="/" onClick={()=>{props.setUser("null")}}> Log Out</Link>
            </Grid>
            
          </Grid>
        </Box>
    </section>
    ] : props.user === "doctor" ?[
      <section>
       <Box className="navbar">
          <Grid container alignItems="center">
            <Grid item xs={3} >
                <Link className='menuButton' to="/patients">Patients </Link>
            </Grid>
            <Grid item xs={3} >
                <Link className='menuButton' to="/examination"> Examination</Link>
            </Grid>
            <Grid item xs={3} >
              <Link className='menuButton' to="/profile"> Profile</Link>
            </Grid>
            <Grid item xs={3} >
              <Link className='menuButton' to="/" onClick={()=>{props.setUser("null")}}> Log Out</Link>
            </Grid>
          </Grid>
        </Box>
    </section>
    ] : [
      <section>

      </section>
    ]
  );
}

export default Navbar