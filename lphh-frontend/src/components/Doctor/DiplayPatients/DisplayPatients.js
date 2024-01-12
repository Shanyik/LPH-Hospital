import { useContext, useEffect, useState } from "react";
import "./DisplayPatients.css";
import { Box, Grid } from "@mui/material";
import SearchField from "../SearchPatient";
import ExaminationTable from "./ExaminationTable";
import PresceptionTable from "./PrescriptionTable";
import { FetchErrorContext } from "../../401Redirect/fetchErrorContext";

const DisplayPatients = (props) => {

    const deletePatient = (username) => {
        return fetch(`/api/Patient/Delete:${username}`, {
            method: 'DELETE',
        })
            .then((res) => res.text())
            .then((res) => console.log(res))
    }

    const getExamDataByPatientID = (id) => {
        return fetch(`/api/Exam/GetByPatientId:${id}`, {
            method: 'GET',
        }).then((res => res.json())) //proxy miatt -->json-ben
    }

    const getPresceptionDataByPatientID = (id) => {
        console.log(id)
        return fetch(`/api/Prescription/GetByPatientId:${id}`, {
            method: 'GET',
        }).then((res => res.json()))
    }

    const getAllDoctors = () => {
        return fetch('/api/Doctor/GetAll', {
            method: 'GET',
        }).then(res => res.json())
    }

    const findPatient = (medNumber) => {
        return fetch(`/api/Patient/GetByMedicalNumber:${medNumber}`, {
            method: 'GET',
        }).then(res => res.json())
    }

    const getAllPatient = () => {
        return fetch('/api/Patient/GetAll', {
            method: 'GET',
        }).then(res => res.json())
    }

    const [patients, setPatients] = useState([])
    const [searchValue, setSearchValue] = useState("")
    const [searchFetchValue, setSearchFetchValue] = useState(200);
    const [examinations, setExaminations] = useState([]);
    const [presciptions, setPresciptions] = useState([]);
    const [doctors, setDoctors] = useState([]);

    const { originUrl, setOriginUrl, originOptions, setOriginOptions, dataSetter, setDataSetter } = useContext(FetchErrorContext)

    useEffect(() => {
        getAllPatient().then(data => {
            setPatients(data);
            return getAllDoctors();
        }).then(data => {
            console.log(data)
            setDoctors(data);
        }).catch(error => console.log(error));
    }, [])

    const deleteButton = (username) => {
        deletePatient(username)
            .then(
                setPatients((patients => {
                    return patients.filter((patient) => patient.username !== username)
                })))
    }

    const searchButton = () => {
        const trimmedSearchValue = searchValue.trim();
        if (trimmedSearchValue !== "") {
            findPatient(searchValue).then(data => {
                if (data.status === 404) {
                    setSearchFetchValue(404)
                } else {
                    setPatients([data])
                    setSearchFetchValue(200)
                }
            })
        }
    }

    const refresPatient = () => {
        getAllPatient().then(data => {
            setPatients(data)
        }).catch(
            error => console.log(error)
        )
        setSearchFetchValue(200)
    }

    const rowController = (id) => {
        getExamDataByPatientID(id).then(data => {
            setExaminations(data);
        }).then(
            getPresceptionDataByPatientID(id).then(data => {
                console.log(data)
                setPresciptions(data)
            })
        )

    }


    return (

        <div id="container">
            <div id="displayPatientSearchContainer">
                <span>
                    <SearchField searchButton={searchButton} setSearchValue={setSearchValue} />
                    <button onClick={() => refresPatient()}>Refress</button>
                </span>
            </div>

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
                        <ExaminationTable examinations={examinations} patients={patients} doctor={doctors} />
                        <PresceptionTable presciptions={presciptions} doctors={doctors} patients={patients} cookie={props.cookie} />
                    </Grid>
                </Grid>

            </Box>
        </div>
    )
}

export default DisplayPatients


