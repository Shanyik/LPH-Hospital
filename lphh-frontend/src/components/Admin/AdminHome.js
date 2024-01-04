import { useEffect, useState } from "react";

const getAllDoctors = () => {
    return fetch('/api/Doctor/GetAll', {
        method: 'GET',
    }).then(res => res.json())
}

const AdminHome = () => {

    const [doctors, setDoctors] = useState(null);

    useEffect(() => {
        getAllDoctors().then(
            data => {
                setDoctors(data);
            }
        )
    }, [])
    return (
        <>
            Admin Home


            <table>
                <thead>
                    <tr>
                        <th>Firstname</th>
                        <th>Lastname</th>
                        <th>Email</th>
                        <th>Phone</th>
                        <th>Ward</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        doctors !== null ? (
                            doctors.map(element => (
                                <tr key={element.id}>
                                    <td>{element.firstName}</td>
                                    <td>{element.lastName}</td>
                                    <td>{element.email}</td>
                                    <td>{element.phoneNumber}</td>
                                    <td>{element.ward}</td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="5">Loading...</td>
                            </tr>
                        )
                    }
                </tbody>
            </table>
        </>
    )
}

export default AdminHome;