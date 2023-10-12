import { useEffect, useState } from "react";
import "./DisplayPatients.css";
import { Box, Grid } from "@mui/material";
import SearchField from "../Doctor/SearchPatient";
import ExaminationTable from "../Doctor/ExaminationTable";
import PresceptionTable from "../Doctor/PrescriptionTable";

const deletePatient = (username) => {
    return fetch(`http://localhost:5274/Patient/Delete:${username}`, { method: "DELETE" })
        .then((res) => res.text())
        .then((res) => console.log(res))
}

const getExamDataByPatientID = (id) => {

    return fetch(`http://localhost:5274/Exam/GetByPatientId:${id}`).then((res => res.json()))
}

const getPresceptionDataByPatientID = (id) => {

    return fetch(`http://localhost:5274/Prescription/GetByPatientId:${id}`).then((res => res.json()))
}



const DisplayPatients = () => {

    const [patients, setPatients] = useState([])
    const [searchValue, setSearchValue] = useState("")
    const [searchFetchValue, setSearchFetchValue] = useState(200);
    const [examinations, setExaminations] = useState([]);
    const [presciptions, setPresciptions] = useState([]);


    useEffect(() => {
        fetch('http://localhost:5274/Patient/GetAll') // env
            .then(response => response.json())
            .then(data => {
                setPatients(data)
            }
            )
            .catch(error => console.log(error))
    }, [])

    const deleteButton = (username) => {
        deletePatient(username)
            .then(
                setPatients((patients => {
                    return patients.filter((patient) => patient.username !== username)
                })))
    }
    const findPatient = (username) => {
        return fetch(`http://localhost:5274/Patient/GetByUsername:${username}`)
            .then(res => res.json())
    }
    const searchButton = () => {
        const trimmedSearchValue = searchValue.trim();
        if (trimmedSearchValue !== "") {
            findPatient(searchValue).then(data => {
                console.log(data)
                if(data.status === 404){
                    console.log("asdff")
                    setSearchFetchValue(404)
                }else{
                    console.log("234")
                    setPatients([data])
                    setSearchFetchValue(200)
                }
               
                /*if (!Object.keys(data).includes("message")) {
                    setPatients([data])
                    setSearchFetchValue([])
                } else {
                    setSearchFetchValue([data])
                }*/
            })
        }
    }

    const refresPatient = () => {
        fetch('http://localhost:5274/Patient/GetAll') // env
            .then(response => response.json())
            .then(data => {
                setPatients(data)
            }
            )
            .catch(error => console.log(error))
        setSearchFetchValue(200)
    }

    const rowController = (id) => {
        console.log(id)
        getExamDataByPatientID(id).then(data=> {
            console.log(data)
            setExaminations(data);
        }).then(
            getPresceptionDataByPatientID(id).then(data => {
                console.log(data)
                setPresciptions(data)
            })
        )

    }


    return (
        <div>
            <SearchField searchButton={searchButton} setSearchValue={setSearchValue}  />
            <Box id="patientMain">
                <Grid container alignItems="center">
                    <Grid item xs={8} >
                        
                        {
                            patients.length > 0 ? [
                                <table id="patientTable">
                                    <thead>
                                        <tr>
                                            <th>First Name</th>
                                            <th>Last Name</th>
                                            <th>Email</th>
                                            <th>Phone number</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            searchFetchValue === 404 ? [
                                                <td colSpan="5">Patient not found <div onClick={() => { refresPatient() }}>refress the list</div></td>
                                            ] : [
                                                Object.values(patients).map((patient) => (
                                                    <tr key={patient.id} onClick={()=>{rowController(patient.id)}}>
                                                        <td>{patient.firstName}</td>
                                                        <td>{patient.lastName}</td>
                                                        <td>{patient.email}</td>
                                                        <td>{patient.phoneNumber}</td>
                                                        <td><button onClick={() => deleteButton(patient.username)}>Delete</button></td>
                                                    </tr>
                                                ))
                                            ]
                                        }

                                    </tbody>
                                </table>
                            ] : [
                                <div className="loading">Waiting for Data...</div>
                            ]
                        }

                    </Grid>
                    <Grid item xs={4} >
                        <ExaminationTable examinations={examinations}/>
                        <PresceptionTable presciptions={presciptions}/>
                    </Grid>
                </Grid>

            </Box>
        </div>
    )
}

export default DisplayPatients


