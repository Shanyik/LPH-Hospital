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
            <Grid item xs={4} >
                <Link className='menuButton' to="/">Home </Link>
            </Grid>
            <Grid item xs={4} >
                <Link className='menuButton' to="/"> choiceA</Link>
            </Grid>
            <Grid item xs={4} >
                <Link className='menuButton' to="/"> choiceB</Link>
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
          
            </Grid>
            <Grid item xs={3} >
                <Link className='menuButton' to="/"><button onClick={()=>{props.setUser("null")}}>LogOut</button></Link>
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