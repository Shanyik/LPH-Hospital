import { useEffect, useState } from "react";

const getAllDoctors = (token) => {
    return fetch('/api/Doctor/GetAll', {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        }
    }).then(res => res.json())
}

const AdminHome = (props) => {

    const [doctors, setDoctors] = useState(null);

    useEffect(() => {
        getAllDoctors(props.cookie["token"]).then(
            data => {
                setDoctors(data);
                console.log(data[0])
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