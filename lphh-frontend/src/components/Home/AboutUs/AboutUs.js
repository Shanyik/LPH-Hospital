import React, { useEffect, useState } from 'react'
import "./AboutUs.css"
import { Box, Container, Grid, Typography, Link} from "@mui/material";

const AboutUs = () => {

    const [width, setWidth] = useState()

    useEffect(() => {
        setWidth(window.innerWidth)
        console.log(width)
    }, [])

  return (
    <section className='aboutUs'>
        <Box
          sx={{
            width: "100%",
            height: "auto",
            backgroundColor: "#3e3f40",
            marginTop: "auto"
          }}
        >
            
            <Grid container alignItems="center"  >
              
                <Grid item xs={12} sm={6} >
                    <img className='image' src={require('../../Images/equipment.jpg')}></img>
                </Grid>
                <Grid item xs={12} sm={6} alignItems="center">
                    <p className='aboutText'>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec in nisl at nibh elementum eleifend sed eget sapien. Praesent euismod, tortor at dictum lacinia, lacus sem cursus felis, sit amet hendrerit tortor turpis maximus dui.
                    </p>
                </Grid>

                {width <=601 ? [
                    <>
                    <Grid item xs={12} sm={6}>
                        <img className='image' src={require('../../Images/equipment.jpg')}></img>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <p className='aboutText'>
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec in nisl at nibh elementum eleifend sed eget sapien. Praesent euismod, tortor at dictum lacinia, lacus sem cursus felis, sit amet hendrerit tortor turpis maximus dui.
                        </p>
                    </Grid>
                    
                    </>
                ] : [
                    <>
                    <Grid item xs={12} sm={6}>
                        <p className='aboutText'>
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec in nisl at nibh elementum eleifend sed eget sapien. Praesent euismod, tortor at dictum lacinia, lacus sem cursus felis, sit amet hendrerit tortor turpis maximus dui.
                        </p>
                    </Grid>
                        <Grid item xs={12} sm={6}>
                            <img className='image' src={require('../../Images/equipment.jpg')}></img>
                    </Grid>
                    </>
                ]}

                
              
                <Grid item xs={12} sm={6}>
                    <img className='image' src={require('../../Images/equipment.jpg')}></img>
                </Grid>
                <Grid item xs={12} sm={6}>
                    <Typography color={"white"} fontSize={"25px"} margin={"3rem"}>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec in nisl at nibh elementum eleifend sed eget sapien. Praesent euismod, tortor at dictum lacinia, lacus sem cursus felis, sit amet hendrerit tortor turpis maximus dui.
                    </Typography>
                </Grid>
            </Grid>
        </Box>
    </section>
  )
}

export default AboutUs