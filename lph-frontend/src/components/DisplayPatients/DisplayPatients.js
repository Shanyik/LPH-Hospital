import { useEffect, useState } from "react"
import "./DisplayPatients.css"

const deletPatient = (username) => {
    return fetch(`http://localhost:5274/Patient/DeletByUsername:${username}`, { method: "DELETE" }).then((res) =>
        res.text()
    );
}

const DisplayPatients = () => {

    const [patients, setPatients] = useState([])

    useEffect(() => {
        fetch('http://localhost:5274/Patient/GetAll')
            .then(response => response.json())
            .then(data => setPatients(data))
            .catch(error => console.log(error))
    }, [])

    const deletButton = (username) => {
        deletPatient(username).then(data => console.log(data));
        console.log(username)
        setPatients((patients) => {
            return patients.filter((patient) => patient.username !== username);
        });
    }

    return (
        (patients.length > 0) ? [
            <table>
                <thead>
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Email</th>
                        <th>Phone number</th>
                    </tr>
                </thead>
                <tbody>
                    {patients.map((patient, i) => (
                        <tr key={patient.id} >
                            <td>{patient.firstName}</td>
                            <td>{patient.lastName}</td>
                            <td>{patient.email}</td>
                            <td>{patient.phoneNumber}</td>
                            <td><button type="button" onClick={() => deletButton(patient.username)}>
                                Delete
                            </button></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        ] : [
            <div className="loading">Waiting for Data...</div>
        ]
    )
}

export default DisplayPatients


