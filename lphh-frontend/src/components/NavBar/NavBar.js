import React, { useContext, useEffect, useState } from 'react';
import "./NavBar.css"
import { Box, Grid } from "@mui/material";
import { Link, useNavigate } from 'react-router-dom'
import logo from '../Images/lphh-minilogo.png';
import { FetchErrorContext } from '../401Redirect/fetchErrorContext';



const Navbar = (props) => {

  const [logout, setLogout] = useState(false);

  const navigate = useNavigate()
  const { refresh, setRefresh, setId } = useContext(FetchErrorContext)


  useEffect(()=> {
    if(logout === false && refresh === undefined || refresh === null){
      let role = localStorage.getItem('role');
      console.log(role)
      setRefresh(role);
    }
  },[refresh])

  const handleLogOut = async () => {
    localStorage.removeItem('id');
    localStorage.removeItem('role');
    fetch('api/Auth/Logout', {
      method: 'GET'
    }).then(response => {if(response.status !== 200){response.json()}})
      .catch(error => console.log(error))
    setRefresh(null);
    setId(null);
    
    navigate("/");
    
  }

  return (
    refresh === "Patient" ? (
      <section>
        <Box className="navbar">
          <Grid container alignItems="center">
            <Grid item xs={1} >
              <img src={logo} alt="Logo" className="mini-logo" />
            </Grid>
            <Grid item xs={0.1} >
            </Grid>
            <Grid xs={0.3} >
              <p> | </p>
            </Grid>
            <Grid item xs={0.1} >
            </Grid>
            <Grid item xs="auto" >
              <Link className='menuButton' to="/patient/home">Home</Link>
            </Grid>

            <Grid item xs="auto">
              <Link className='menuButton' to="/patient/prescriptions">Prescriptions</Link>
            </Grid>

            <Grid item xs="auto" >
              <Link className='menuButton' to="/patient/documents">Documents</Link>
            </Grid>
            <Grid item xs={0.3} >
            </Grid>
            <Grid item xs={0.3} >
              <p> | </p>
            </Grid>
            <Grid item xs={0.3} >
            </Grid>
            <Grid item xs="auto" s >
              <Link className='menuButton' to="/profile"> Profile</Link>
            </Grid>
            <Grid item xs="auto" >
              <Link className='menuButton' to="/" onClick={() => { handleLogOut() }}> Log Out</Link>
            </Grid>

          </Grid>
        </Box>
      </section>
    ) : refresh === "Doctor" ? (
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
              <Link className='menuButton' to="/" onClick={() => { handleLogOut() }}> Log Out</Link>
            </Grid>
          </Grid>
        </Box>
      </section>
    ) : refresh === "Admin" ? (
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
              <Link className='menuButton' to="/adminHome"> Home</Link>
            </Grid>
            <Grid item xs={3} >
              <Link className='menuButton' to="/" onClick={() => { handleLogOut() }}> Log Out</Link>
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