import React from 'react';
import "./NavBar.css"
import { Box, Grid } from "@mui/material";
import { Link, useNavigate } from 'react-router-dom'
import logo from '../Images/lphh-minilogo.png';


const Navbar = (props) => {

  const navigate = useNavigate()

  const handleLogOut = async () => {
    await props.removeCookie("id", {path: "/"})
    await props.removeCookie("role", {path: "/"})
    navigate("/")
    window.location.reload(true);
    
  }

  return (
    props.cookie["role"] === "Patient" ? (
      <section>
       <Box className="navbar">
          <Grid container alignItems="center">
          <Grid item xs={1} >
              <img src={logo} alt="Logo" className="mini-logo" />
            </Grid>
            <Grid item xs={2} >
                <Link className='menuButton' to="/patient/home">Home</Link>
            </Grid>
            <Grid item xs={2} >
                <Link className='menuButton' to="/patient/prescriptions">Prescriptions</Link>
            </Grid>
            <Grid item xs={2} >
                <Link className='menuButton' to="/patient/documents">Documents</Link>
            </Grid>
            <Grid item xs={1} >
            </Grid>
            <Grid item xs={2} >
                <Link className='menuButton' to="/profile"> Profile</Link>
            </Grid>
            <Grid item xs={2} >
                <Link className='menuButton' to="/" onClick={()=>{handleLogOut()}}> Log Out</Link>
            </Grid>
            
          </Grid>
        </Box>
    </section>
    ) : props.cookie["role"] === "Doctor" ?(
      <section>
       <Box className="navbar">       
          <Grid container alignItems="center">
            <Grid item xs={1} >
              <img src={logo} alt="Logo" className="mini-logo" />
            </Grid>
            <Grid item xs={2.2} >
                <Link className='menuButton' to="/patients">Patients </Link>
            </Grid>
            <Grid item xs={2.2} >
                <Link className='menuButton' to="/examination"> Examination</Link>
            </Grid>
            <Grid item xs={2.2} >
                <Link className='menuButton' to="/prescriptioncreator">Prescription </Link>
            </Grid>
            <Grid item xs={2.2} >
              <Link className='menuButton' to="/profile"> Profile</Link>
            </Grid>
            <Grid item xs={2.2} >
              <Link className='menuButton' to="/" onClick={()=>{handleLogOut()}}> Log Out</Link>
            </Grid>
          </Grid>
        </Box>
    </section>
    ) : props.cookie["role"] === "Admin" ? (
      <section>
        <Box className="navbar">       
          <Grid container alignItems="center">
            <Grid item xs={1} >
              <img src={logo} alt="Logo" className="mini-logo" />
            </Grid>
            <Grid item xs={4} >
                <Link className='menuButton' to="/registration">Doctor registration </Link>
            </Grid>
            <Grid item xs={4} >
              <Link className='menuButton' to="/profile"> Profile</Link>
            </Grid>
            <Grid item xs={3} >
              <Link className='menuButton' to="/" onClick={()=>{handleLogOut()}}> Log Out</Link>
            </Grid>
          </Grid>
        </Box>
      </section>
    ) : (
      null
    )
  );
}

export default Navbar