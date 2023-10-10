import { Box, Grid } from "@mui/material";


const DoctorMain = () => {


    return (
        <>
            <Box className="MainMenu">
                <Grid container alignItems="center">
                    <Grid item xs={8} >
                        main
                    </Grid>
                    <Grid item xs={4} >
                        side
                    </Grid>
                </Grid>
               
            </Box>
        </>
    )
}

export default DoctorMain;