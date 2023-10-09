import { useEffect, useState } from "react"
import "./DisplayDoctors.css"

const DisplayDoctors = () => {

    const deleteButton = (username) =>{
        deleteDoctor(username)
        .then( 
            setDoctors((doctors => {
            return doctors.filter((doctor) => doctor.username !== username) 
        })))
    }

     const deleteDoctor = (username) =>{
        return fetch(`http://localhost:5274/Doctor/Delete:${username}`, {method: "DELETE"})
        .then((res) => res.text())
        .then((res) => console.log(res))
    }

    const [doctors, setDoctors] = useState([])
    
    useEffect(() => {
      fetch('http://localhost:5274/Doctor/GetAll') // env
        .then(response => response.json())
        .then(data => setDoctors(data))
        .catch(error => console.log(error))
    }, [])

  return (
    doctors.length > 0 ? [
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
                {doctors.map((doctor) => (
                    <tr key={doctor.id} >
                        <td>{doctor.firstName}</td>
                        <td>{doctor.lastName}</td>
                        <td>{doctor.email}</td>
                        <td>{doctor.phoneNumber}</td>
                        <td><button onClick={() => deleteButton(doctor.username)}>Delete</button></td>
                    </tr>
                ))}
            </tbody>
        </table>
    ] : [
        <div className="loading">Waiting for Data...</div>
    ] 
  )
}

export default DisplayDoctors

/*



*/

