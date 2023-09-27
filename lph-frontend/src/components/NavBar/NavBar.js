import React from 'react';
import "./NavBar.css"
import { Box, Grid } from "@mui/material";
import { Link } from 'react-router-dom'

const Navbar = () => {
  return (
    <section>
       <Box className="navbar">
          <Grid container alignItems="center">
            <Grid item xs={6} >
                <Link className='menuButton' to="/">Home </Link>
            </Grid>
            <Grid item xs={6} >
                <Link className='menuButton' to="/patients"> Patients</Link>
            </Grid>
          </Grid>

        </Box>
    </section>
  );
}

export default Navbar