
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./ExaminationCreater.css"
import Typography from '@mui/material/Typography';

const getDoctorById = (userId) => {
    return fetch(`/Doctor/GetById:${userId}`).then(res => res.json())
}

const getPatientByMedicalNumber = (medicalNumber) => {
    return fetch(`/Patient/GetByMedicalNumber:${medicalNumber}`).then(res => res.json())
}

const addExam = (data) => {
    return fetch(`/Exam/Add`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    }).then((res) => console.log("success"));
}



const ExaminationCreater = ({ userId }) => {

    const navigate = useNavigate();

    const [doctor, setDoctor] = useState(null)
    const [type, setType] = useState(null)
    const [doctorFirstName, setDoctorFirstName] = useState("")
    const [doctorLastName, setDoctorLastName] = useState("")
    const [wards, setWards] = useState(["a", "b", "c", "d"])
    const [currentWard, setCurrentWard] = useState(null)

    const [patient, setPatient] = useState(null)
    const [patientFirstName, setPatientFirstName] = useState("")
    const [patientLastName, setPatientLastName] = useState("")
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
    }, [])

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
        } else {
            setMedicalNumberError(true)
        }
    };

    const onSubmit = (e) => {
        e.preventDefault();
        console.log(description)
       if(patient !== null && type !== null){
        const data = {
            type: type,
            doctorid: userId,
            patientId: patient.id,
            result: description,
            createdAt : new Date()
        }
        console.log(data)
        setFormError(false)
        addExam(data)
       }else{
        setFormError(true)
       }
       navigate("/patients");
    };



    return (
        doctor !== null ? [<div id="examCreaterContainer">
            <form className="ExamForm" onSubmit={onSubmit}>
                <div id="doctorContainre">
                    <h4>Exam:</h4>
                    <div className="control">
                        <Typography htmlFor="name">ExamType:</Typography>
                        <input
                            placeholder="ExamType"
                            onChange={(e) => setType(e.target.value)}
                            name="type"
                            id="type"
                        />
                    </div>

                    <h4>Doctor:</h4>
                    <div className="control">
                        <Typography htmlFor="name">Firstname:</Typography>
                        <input
                            value={doctor.firstName}
                            placeholder="Fristname"
                            onChange={(e) => setDoctorFirstName(e.target.value)}
                            name="doctorFristName"
                            id="doctorFristName"
                        />
                    </div>
                    <div className="control">
                        <Typography htmlFor="name">Lastname:</Typography>
                        <input
                            value={doctor.lastName}
                            placeholder="Lastname"
                            onChange={(e) => setDoctorLastName(e.target.value)}
                            name="doctorLastName"
                            id="doctorLastName"
                        />
                    </div>
                    <div className="control">

                        <div>{currentWard === "Chose wards" || currentWard === null ? <>Chose wards!</> : <></>}</div>

                        <Typography htmlFor="name">Ward:</Typography>
                        <select onChange={(e) => setCurrentWard(e.target.value)} >
                            <>
                                <option selected={doctor.ward}>Chose wards</option>
                                {wards.map((val, i) => {
                                    return <option key={i} value={val} id={i}>{val}</option>
                                })}

                            </>
                        </select>
                    </div>
                </div>
                <div id="PatientContainer">
                    <h4>Patient:</h4>
                    <div className="control">
                        <Typography htmlFor="name">Firstname:</Typography>
                        <input
                            placeholder="Fristname"
                            onChange={(e) => setPatientFirstName(e.target.value)}
                            name="patientFristName"
                            id="patientFristName"
                        />
                    </div>
                    <div className="control">
                        <Typography htmlFor="name">Lastname:</Typography>
                        <input
                            placeholder="Lastname"
                            onChange={(e) => setPatientLastName(e.target.value)}
                            name="patientLastName"
                            id="patientLastName"
                        />
                    </div>
                    <div className="control">
                        <div>{medicalNumbererror === true ? (
                            <>Only number allowed</>
                        ) : medicalNumbererror === "accept" ? (
                            <>Valid Medical Number</>
                        ) : (
                            <>Required field!</>
                        )}</div>
                        <Typography htmlFor="name">MedicalNumber:</Typography>
                        <input
                            onChange={(e) => handleMedicalNumberChange(e)}
                            name="medicalNumber"
                            id="medicalNumber"
                        />
                    </div>
                </div>
                <div id="descriptionContainer">
                    <div className="control">
                        <h4>Description:</h4>
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