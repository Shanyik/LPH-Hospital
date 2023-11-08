import { useEffect, useState } from "react";
import "./DisplayPatients.css";
import { Box, Grid } from "@mui/material";
import SearchField from "../Doctor/SearchPatient";
import ExaminationTable from "../Doctor/ExaminationTable";
import PresceptionTable from "../Doctor/PrescriptionTable";

const DisplayPatients = (props) => {

    const deletePatient = (username) => {
        return fetch(`api/Patient/Delete:${username}`, {
            method: 'DELETE',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        })
            .then((res) => res.text())
            .then((res) => console.log(res))
    }
    
    const getExamDataByPatientID = (id) => {
        return fetch(`api/Exam/GetByPatientId:${id}`, {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        }).then((res => res.json())) //proxy miatt -->json-ben
    }
    
    const getPresceptionDataByPatientID = (id) => {
    
        return fetch(`api/Prescription/GetByPatientId:${id}`, {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        }).then((res => res.json()))
    }
    
    const getAllDoctors = () => {
        return fetch(`api/Doctor/GetAll`, {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        }).then(res => res.json())
    }

    const [patients, setPatients] = useState([])
    const [searchValue, setSearchValue] = useState("")
    const [searchFetchValue, setSearchFetchValue] = useState(200);
    const [examinations, setExaminations] = useState([]);
    const [presciptions, setPresciptions] = useState([]);
    const [doctors, setDoctors] = useState([]);
    
    useEffect(() => {
        fetch(`$api/Patient/GetAll`, {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        })
            .then(response => response.json())
            .then(data => {
                setPatients(data)
            }
            )
            .catch(error => console.log(error)).then(
                getAllDoctors().then(data => {
                    console.log(data)
                    setDoctors(data)
                })
            )
    }, [])

    const deleteButton = (username) => {
        deletePatient(username)
            .then(
                setPatients((patients => {
                    return patients.filter((patient) => patient.username !== username)
                })))
    }
    const findPatient = (username) => {
        return fetch(`api/Patient/GetByUsername:${username}`, {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        })
            .then(res => res.json())
    }
    const searchButton = () => {
        const trimmedSearchValue = searchValue.trim();
        if (trimmedSearchValue !== "") {
            findPatient(searchValue).then(data => {
                console.log(data)
                if (data.status === 404) {
                    console.log("asdff")
                    setSearchFetchValue(404)
                } else {
                    console.log("234")
                    setPatients([data])
                    setSearchFetchValue(200)
                }
            })
        }
    }

    const refresPatient = () => {
        fetch(`api/Patient/GetAll`, {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        }) // env
            .then(response => response.json())
            .then(data => {
                setPatients(data)
            }
            )
            .catch(error => console.log(error))
        setSearchFetchValue(200)
    }

    const rowController = (id) => {
        getExamDataByPatientID(id).then(data => {
            setExaminations(data);
        }).then(
            getPresceptionDataByPatientID(id).then(data => {
                setPresciptions(data)
            })
        )

    }


    return (

        <div id="container">
           
            <SearchField searchButton={searchButton} setSearchValue={setSearchValue} />
            <Box id="patientMain">
                <Grid container alignItems="center">
                    <Grid item xs={8} >

                        {
                            patients.length > 0 ? [
                                <div id="patientTableContainer">
                                    <table id="patientTable">
                                        <thead>
                                            <tr>
                                                <th>First Name</th>
                                                <th>Last Name</th>
                                                <th>Medical Number</th>
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
                                                        <tr key={patient.id} onClick={() => { rowController(patient.id) }}>
                                                            <td>{patient.firstName}</td>
                                                            <td>{patient.lastName}</td>
                                                            <td>{patient.medicalNumber}</td>
                                                            <td>{patient.phoneNumber}</td>
                                                            <td><button onClick={() => deleteButton(patient.username)}>Delete</button></td>
                                                        </tr>
                                                    ))
                                                ]
                                            }

                                        </tbody>
                                    </table>
                                </div>

                            ] : [
                                <div className="loading">Waiting for Data...</div>
                            ]
                        }

                    </Grid>
                    <Grid item xs={4} >
                        <ExaminationTable examinations={examinations} patients={patients} doctor={doctors}/>
                        <PresceptionTable presciptions={presciptions} doctors={doctors} patients={patients} cookie={props.cookie}/>
                    </Grid>
                </Grid>

            </Box>
        </div>
    )
}

export default DisplayPatients


