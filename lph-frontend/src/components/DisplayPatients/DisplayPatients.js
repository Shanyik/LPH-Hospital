import { useEffect, useState } from "react"
import "./DisplayPatients.css"

const DisplayPatients = () => {

    const deleteButton = (id) =>{
        deletePatient(id).then(data => console.log(data))
        setPatients((patients => {
            return patients.filter((patient) => patient.id !== id) 
        }))
    }

    const deletePatient = (id) =>{
        return fetch(`http://localhost:5274/Patient/Delete:${id}`, {method: "DELETE"})
        .then((res) => res.json())
    }

    const [patients, setPatients] = useState([])
    
    useEffect(() => {
      fetch('http://localhost:5274/Patient/GetAll')
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
                        <td><button onClick={() => deleteButton(patient.id)}>Delete</button></td>
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

