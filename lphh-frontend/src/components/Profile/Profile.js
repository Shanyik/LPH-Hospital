import React, { useContext, useEffect, useState } from 'react'
import { Box, Grid, Avatar, Typography, Button } from "@mui/material"
import "./Profile.css"
import { fetchWithInterceptor } from '../401Redirect/AuthRedirect'
import { FetchErrorContext } from '../401Redirect/fetchErrorContext'



const Profile = (props) => {

  const deleteButton = (username) => {
    console.log(username)

    if (props.cookie["role"] === "Patient") {
      return fetch(`api/Patient/Delete:${username}`, {
        method: 'DELETE'
      })
        .then((res) => res.text())
        .then((res) => console.log(res))
    } else {
      return fetch(`api/Doctor/Delete:${username}`, {
        method: 'DELETE'
      })
        .then((res) => res.text())
        .then((res) => console.log(res))
    }
  }

  const [userData, setUserData] = useState(null)
  const { originUrl, setOriginUrl, originOptions, setOriginOptions, dataSetter, setDataSetter } = useContext(FetchErrorContext)

  useEffect(() => {
    if (props.cookie["role"] === "Patient") {
      fetch(`api/Patient/GetById:${props.cookie["id"]}`, {
        method: 'GET'
      })
        .then(response => response.json())
        .then(data => {
          console.log(data)
          setUserData(data);
        }
        )
        .catch(error => console.log(error))
    }
    else {
      fetchWithInterceptor(`api/Doctor/GetById:${props.cookie["id"]}`, {
        method: 'GET'
      }, setOriginUrl(`api/Doctor/GetById:${props.cookie["id"]}`), setOriginOptions({
        method: 'GET'
      }))
        /*.then(response => { if (response.status === 200) {
            console.log("ok")
           return response.json() }else{
            console.log("ok1")
           } })*/
        .then(data => {
          console.log(data)
          setUserData(data);
        }
        )
        .catch(error => console.log(error))
    }
  }, [props.cookie])

  return (
    userData && userData !== null  ? (
      <>
        <Avatar className="profileAvatar" sx={{ bgcolor: "orange", width: 100, height: 100, alignItems: "center", left: "47%", top: "15%", zIndex: "100", position: "absolute" }}>{Array.from(userData.username)[0]}</Avatar>
        <Box className="profileBox" alignItems={"center"} >
          <Grid sx={{ width: "100%", height: "80%", position: "absolute", marginTop: "10%" }} container alignItems="center">
            <Grid item xs={6} >
              <Typography fontWeight={'bold'} sx={{ fontSize: "30px" }}>
                First Name
              </Typography>
              <Typography sx={{ fontSize: "25px" }}>
                {userData.firstName}
              </Typography>
            </Grid>
            <Grid item xs={6} >
              <Typography fontWeight={'bold'} sx={{ fontSize: "30px" }}>
                Last Name
              </Typography>
              <Typography sx={{ fontSize: "25px" }}>
                {userData.lastName}
              </Typography>
            </Grid>
            {props.cookie["role"] === "Patient" ? [
              <Grid item xs={6} >
                <Typography fontWeight={'bold'} sx={{ fontSize: "30px" }}>
                  Medical Number
                </Typography>
                <Typography sx={{ fontSize: "25px" }}>
                  {userData.medicalNumber}
                </Typography>
              </Grid>
            ] : [
              <Grid item xs={6} >
                <Typography fontWeight={'bold'} sx={{ fontSize: "30px" }}>
                  Ward
                </Typography>
                <Typography sx={{ fontSize: "25px" }}>
                  {userData.ward}
                </Typography>
              </Grid>
            ]}
            <Grid item xs={6} >
              <Typography fontWeight={'bold'} sx={{ fontSize: "30px" }}>
                Email
              </Typography>
              <Typography sx={{ fontSize: "25px" }}>
                {userData.email}
              </Typography>
            </Grid>
            <Grid item xs={6} >
              <Typography fontWeight={'bold'} sx={{ fontSize: "30px" }}>
                PhoneNumber
              </Typography>
              <Typography sx={{ fontSize: "25px" }}>
                {userData.phoneNumber}
              </Typography>
            </Grid>
            <Grid item xs={6} >
              <Typography fontWeight={'bold'} sx={{ fontSize: "30px" }}>
                Username:
              </Typography>
              <Typography sx={{ fontSize: "25px" }}>
                {userData.username}
              </Typography>
            </Grid>
            <Grid item xs={12} >
              <Button variant="contained" color="error" href="/" onClick={() => deleteButton(userData.username)}>
                Delete
              </Button>
            </Grid>
          </Grid>
        </Box>
      </>
    ) : (
      <Box className="profileBox" alignItems={"center"} >
        <div>Loading Data...</div>
      </Box>
    )

  )
}

export default Profile

