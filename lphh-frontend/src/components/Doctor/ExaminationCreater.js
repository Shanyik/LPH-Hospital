import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./ExaminationCreater.css"
import Typography from '@mui/material/Typography';

const ExaminationCreater = ({ userId }) => {

    const getDoctorById = (userId) => {
        return fetch(`api/Doctor/GetById:${userId}`, {
            method: 'GET',
        }).then(res => res.json())
    }
    
    const getPatientByMedicalNumber = (medicalNumber) => {
        return fetch(`api/Patient/GetByMedicalNumber:${medicalNumber}`, {
            method: 'GET',
        }).then(res => res.json())
    }
    
    const addExam = (data) => {
        return fetch(`api/Exam/Add`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data),
        }).then((res) => console.log("success"));
    }

    const navigate = useNavigate();

    const [doctor, setDoctor] = useState(null)
    const [type, setType] = useState(null)
    const [doctorFirstName, setDoctorFirstName] = useState("")
    const [doctorLastName, setDoctorLastName] = useState("")
    const [patient, setPatient] = useState(null)
    const [medicalNumber, setMedicalNumber] = useState("")
    const [medicalNumbererror, setMedicalNumberError] = useState(false)

    const [description, setDescription] = useState("")

    const [formError, setFormError] = useState(false)


    useEffect(() => {
        getDoctorById(userId).then(data => {
            setDoctor(data)
            console.log(data.lastName)
            setDoctorFirstName(data.firstName)
            setDoctorLastName(data.lastName)
        })
    }, [userId])

    const handleMedicalNumberChange = (event) => {
        const input = event.target.value;
        const isNumeric = /^[0-9-]+$/.test(input);

        if (isNumeric) {
            setMedicalNumber(input);
            setMedicalNumberError(false)
            getPatientByMedicalNumber(input).then(data => {
                if (data.status === 404) {
                    console.log(data)
                } else {
                    console.log(data)
                    setPatient(data)
                    setMedicalNumberError("accept")
                }
            })
        } else if (input === "") {
            setMedicalNumberError(false)
            setPatient(null)
        } else {
            setMedicalNumberError(true)
            setPatient(null)
        }
        setPatient(null)
    };

    const onSubmit = (e) => {
        e.preventDefault();
        console.log(description)
        if (patient !== null && type !== null) {
            const data = {
                type: type,
                doctorid: userId,
                patientId: patient.id,
                result: description,
                createdAt: new Date()
            }
            console.log(data)
            setFormError(false)
            addExam(data)
            navigate("/patients");
        } else {
            setFormError(true)
        }
       
    };



    return (
        doctor !== null ? [<div id="examCreaterContainer">
            <form className="ExamForm" onSubmit={onSubmit}>
                <div id="doctorContainre">
                    <h4>Exam</h4>
                    <div className="control">
                        <Typography htmlFor="name">ExamType</Typography>
                        <input
                            placeholder="ExamType"
                            onChange={(e) => setType(e.target.value)}
                            name="type"
                            id="type"
                        />
                    </div>

                    
                </div>
                <div id="PatientContainer">
                    <h4>Patient</h4>

                    <div className="control">
                        <div>{medicalNumbererror === true ? (
                            <>Only number allowed</>
                        ) : medicalNumbererror === "accept" ? (
                            <div id="okMedicalNumber">Valid Medical Number</div>
                        ) : (
                            <>Required field!</>
                        )}</div>
                        <Typography htmlFor="name">MedicalNumber</Typography>
                        <input
                            onChange={(e) => handleMedicalNumberChange(e)}
                            name="medicalNumber"
                            id="medicalNumber"
                        />
                    </div>
                    {
                        patient !== null ? [
                            <div id="patientDataContainer">
                                <div className="control">
                                    <Typography htmlFor="name">Firstname</Typography>
                                    <div id="patinetData">
                                        {patient.firstName}
                                    </div>
                                </div>
                                <div className="control">
                                    <Typography htmlFor="name">Lastname</Typography>
                                    <div id="patinetData">
                                        {patient.lastName}
                                    </div>
                                </div>
                            </div>
                        ] : [
                            <></>
                        ]
                    }


                </div>
                <div id="descriptionContainer">
                    <div className="control">
                        <h4>Description</h4>
                        <textarea
                            className="descriptionTextArea"
                            onChange={(e) => setDescription(e.target.value)}
                            name="medicalNumber"
                            id="medicalNumber"
                        />
                    </div>
                </div>
                <div className="buttons">
                    {
                        formError === true ? <div>Please fill every required field!</div> : <></>
                    }
                    <button type="submit">
                        Submit
                    </button>

                </div>
            </form>

        </div>] : [<div>Loading...</div>]


    )
}

export default ExaminationCreater;