import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./ExaminationCreater.css"
import Typography from '@mui/material/Typography';
import { FetchErrorContext } from "../../401Redirect/fetchErrorContext";
import { fetchWithInterceptor } from "../../401Redirect/AuthRedirect";

const ExaminationCreater = ({ userId }) => {

    const getDoctorById = (userId) => {
        return fetchWithInterceptor(`api/Doctor/GetById:${userId}`, {
            method: 'GET',
        }, setOriginUrl(`api/Doctor/GetById:${userId}`), setOriginOptions({
            method: 'GET',
        }))
    }
    
    const getPatientByMedicalNumber = (medicalNumber) => {
        return fetchWithInterceptor(`api/Patient/GetByMedicalNumber:${medicalNumber}`, {
            method: 'GET',
        }, setOriginUrl(`api/Patient/GetByMedicalNumber:${medicalNumber}`), setOriginOptions({
            method: 'GET',
        }))
    }
    
    const addExam = (data) => {
        return fetchWithInterceptor(`api/Exam/Add`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data),
        }, setOriginUrl(`api/Exam/Add`), setOriginOptions( {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data),
        }))
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
    const { originUrl, setOriginUrl, originOptions, setOriginOptions, dataSetter, setDataSetter } = useContext(FetchErrorContext)

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
        const validInput = isInputValid(input, event);

        if (validInput.length === 11) {
            setMedicalNumber(input);
            setMedicalNumberError(false);
            getPatientByMedicalNumber(input).then(data => {
                if (data.status === 404) {
                    setMedicalNumberError("invalid");
                } else {
                    console.log(data);
                    setPatient(data);
                    setMedicalNumberError("accept");
                }
            })
        } else if (input === "") {
            console.log("asd1")
            setMedicalNumberError(false);
            setPatient(null);
        }else {
            setMedicalNumberError("short&true");
        }
        setPatient(null);
    };

    const isInputValid = (input, e) => {

        const pattern = /^[0-9]+$/;
        const removedValue = removeWhiteSpaces(input);

        if(pattern.test(removedValue)){
            let newValue = addWhiteSpaces(removedValue);
            e.target.value = newValue;
            return newValue;
        }else{
            const removedValue =  input.replace(/[^0-9]/g, '');
            e.target.value =addWhiteSpaces(removedValue);
            return addWhiteSpaces(removedValue);
        }

    }

    const removeWhiteSpaces = (input) => {
        const indexes = [3, 7];
        let newString = "";
        for (let index = 0; index < input.length; index++) {
            if (!indexes.includes(index) || input[index] !== "-") {
                newString += input[index];
            }

        }

        return newString;
    }

    const addWhiteSpaces = (input) => {
        const indexes = [3, 6];
        console.log(input)
        let newString = "";
        for (let index = 0; index < input.length; index++) {
            if (indexes.includes(index)) {
                newString += "-" + input[index];
            } else {
                newString += input[index];
            }

        }

        return newString;
    }

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
                        ) : medicalNumbererror === "invalid" ? (
                            <div >Medical Number not exsist</div>
                        ) : medicalNumbererror === "short&true" ? (
                            <div >Medical Number to short and only allowed number!</div>
                        ) : (
                            <>Required field!</>
                        )}</div>
                        <Typography htmlFor="name">MedicalNumber</Typography>
                        <input
                            onChange={(e) => handleMedicalNumberChange(e)}
                            name="medicalNumber"
                            id="medicalNumber"
                            maxLength={11}
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