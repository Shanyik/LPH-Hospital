import { useEffect, useState } from "react"
import "./DisplayPatients.css"

const DisplayPatients = () => {

    const deleteButton = (username) =>{
        deletePatient(username)
        .then( 
            setPatients((patients => {
            return patients.filter((patient) => patient.username !== username) 
        })))
    }

     const deletePatient = (username) =>{
        return fetch(`http://localhost:5274/Patient/Delete:${username}`, {method: "DELETE"})
        .then((res) => res.text())
        .then((res) => console.log(res))
    }

    const [patients, setPatients] = useState([])
    
    useEffect(() => {
      fetch('http://localhost:5274/Patient/GetAll') // env
        .then(response => response.json())
        .then(data => setPatients(data))
        .catch(error => console.log(error))
    }, [])

  return (
    patients.length > 0 ? [
        <table>
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
                {patients.map((patient) => (
                    <tr key={patient.id} >
                        <td>{patient.firstName}</td>
                        <td>{patient.lastName}</td>
                        <td>{patient.email}</td>
                        <td>{patient.phoneNumber}</td>
                        <td><button onClick={() => deleteButton(patient.username)}>Delete</button></td>
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

/*



*/

