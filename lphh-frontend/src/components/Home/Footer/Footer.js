import React from "react";
import { Box, Container, Grid, Typography, Link} from "@mui/material";
import FacebookIcon from '@mui/icons-material/Facebook';
import InstagramIcon from '@mui/icons-material/Instagram';
import PhoneIphoneIcon from '@mui/icons-material/PhoneIphone';
import EmailIcon from '@mui/icons-material/Email';
import LocationOnIcon from '@mui/icons-material/LocationOn';

const Footer = () => {
    return (
      <section className="contact">
        <Box
          sx={{
            width: "100%",
            height: "auto",
            backgroundColor: "#3e3f40",
            paddingTop: "1rem",
            paddingBottom: "1rem",
            marginTop: "auto"
          }}
        >
          <Container maxWidth="100%">
            <Grid container direction="column" alignItems="center">
              <Grid style={{paddingBottom: "1rem", width: "90%", textAlign: "center", color: "white"}} container spacing={2} item xs={12} alignItems="center">
                <Grid item xs={6} sm={4}>
                  <PhoneIphoneIcon style={{ color: "white" , fontSize: 40}}/>
                  <p>+36/30-123456789</p>
                </Grid>
                <Grid item xs={6} sm={4}>
                  <EmailIcon style={{ color: "white" , fontSize: 40}}/>
                  <p>info@lphh.com</p>
                </Grid>
                <Grid item xs={12} sm={4}>
                  <LocationOnIcon style={{ color: "white" , fontSize: 40}}/>
                  <p>Dunaújváros, Gutenberg köz, 2400</p>
                </Grid>
              </Grid>

              <Grid style={{paddingBottom: "2rem", width: "90%"}} item xs={12}>
              <iframe
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d483719.89889785886!2d17.717468238489122!3d46.41893698382447!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47421736f740886d%3A0x8882063c293baa4c!2sLos%20Pollos%20Hermanos!5e0!3m2!1shu!2shu!4v1704883669740!5m2!1shu!2shu"
                width={`100%`}
                height={"350vh"}
                style={{ border: 0 }}
                ></iframe>
              </Grid>

              <Grid sx={{paddingBottom: "1rem"}} item xs={12}>
                <Link href= "https://www.imdb.com/title/tt0903747/">
                  <FacebookIcon sx={{ color: "white" , fontSize: 40, ":hover":{color: "blue"} }}/>
                </Link>
                <Link href= "https://www.imdb.com/title/tt0903747/">
                  <InstagramIcon sx={{ color: "white", fontSize: 40, paddingLeft: "10px", ":hover":{color: "pink"}  }}/>
                </Link>
              </Grid>

              <Grid item xs={12}>
                <Typography color="white " variant="subtitle1">
                  {` © ${new Date().getFullYear()} Los Pollos Hermanos Hospital | created by: LPHH Team`}
                </Typography>
              </Grid>

            </Grid>
          </Container>
        </Box>
      </section>
    );
}

export default Footer