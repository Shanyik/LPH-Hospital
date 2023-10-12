import { useEffect, useState } from "react";


const ExaminationCreater = () => {

    const [type, setType] = useState(null)
    const [doctorFirstName, setDoctorFirstName] = useState("")
    const [doctorLastName, setDoctorLastName] = useState("")
    const [wards, setWards] = useState(["a", "b", "c", "d"])
    const [currentWard, setCurrentWard] = useState(null)

    const [patientFirstName, setPatientFirstName] = useState("")
    const [patientLastName, setPatientLastName] = useState("")
    const [medicalNumber, setMedicalNumber] = useState("")
    const [medicalNumbererror, setMedicalNumberError] = useState(false)

    const [description, setDescription] = useState("")

    const handleSubmit = () => {

    }
    useEffect(()=>{
        console.log(medicalNumbererror)
    })

    const handleMedicalNumberChange = (event) => {
        const input = event.target.value;
        const isNumeric = /^[0-9]+$/.test(input);
        
        if(isNumeric){
            setMedicalNumber(input);
            setMedicalNumberError(false)
        }else if(input === ""){
            setMedicalNumberError(false)
        }else{
            setMedicalNumberError(true)
        }
    };

    const handleDescription = () => {

    }
   

    return (
        <div>
            <form className="ExamForm" onSubmit={handleSubmit}>
                <div id="doctorContainre">
                    <h4>Doctor:</h4>
                    <div className="control">
                        <label htmlFor="name">Firstname:</label>
                        <input
                            placeholder="Fristname"
                            onChange={(e) => setDoctorFirstName(e.target.value)}
                            name="doctorFristName"
                            id="doctorFristName"
                        />
                    </div>
                    <div className="control">
                        <label htmlFor="name">Lastname:</label>
                        <input
                            placeholder="Lastname"
                            onChange={(e) => setDoctorLastName(e.target.value)}
                            name="doctorLastName"
                            id="doctorLastName"
                        />
                    </div>
                    <div className="control">

                        <div>{currentWard === "Chose wards" || currentWard === null ? <>Chose wards!</> : <></>}</div>

                        <label htmlFor="name">Ward:</label>
                        <select onChange={(e) => setCurrentWard(e.target.value)} >
                            <>
                                <option value={"Chose wards"}>Chose wards</option>
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
                        <label htmlFor="name">Firstname:</label>
                        <input
                            placeholder="Fristname"
                            onChange={(e) => setPatientFirstName(e.target.value)}
                            name="patientFristName"
                            id="patientFristName"
                        />
                    </div>
                    <div className="control">
                        <label htmlFor="name">Lastname:</label>
                        <input
                            placeholder="Lastname"
                            onChange={(e) => setPatientLastName(e.target.value)}
                            name="patientLastName"
                            id="patientLastName"
                        />
                    </div>
                    <div className="control">
                        <div>{medicalNumbererror === true ? <>Only number allowed</>: <></>}</div>
                        <label htmlFor="name">MedicalNumber:</label>
                        <input
                            onChange={(e)=>handleMedicalNumberChange(e)}
                            name="medicalNumber"
                            id="medicalNumber"
                        />
                    </div>
                </div>
                <div id="descriptionContainer">
                <div className="control">
                        <h4>Description:</h4>
                        <textarea
                            onChange={(e)=>handleMedicalNumberChange(e)}
                            name="medicalNumber"
                            id="medicalNumber"
                        />
                    </div>
                </div>
            </form>

        </div>
    )
}

export default ExaminationCreater;