import React, { useContext, useEffect, useState } from 'react'
import ModalDocument from './ModalDocument';
import Button from '@mui/material/Button';
import { FetchErrorContext } from '../../401Redirect/fetchErrorContext';
import { fetchWithInterceptor } from '../../401Redirect/AuthRedirect';

const Documents = (props) => {
    const [documents, setDocuments] = useState([])
    const [doctors, setDoctors] = useState([])
    const [showModal, setShowModal] = useState(false)
    const [currentId, setCurrentId] = useState(false)

    const { originUrl, setOriginUrl, originOptions, setOriginOptions, refresh, setRefresh, id, setId } = useContext(FetchErrorContext)


    useEffect(() => {
        fetch(`/api/Exam/GetByPatientId:${id}`, {
            method: 'GET',
        }).then(res => res.json())
            .then(data => {
                console.log("asd")
                setDocuments(data);
                console.log(data);
            })
        fetch('/api/Doctor/GetAll', {
            method: 'GET',
        }).then(res => res.json())
            .then(data => {
                setDoctors(data);
                console.log(data);
            })
            .catch(error => console.log(error))


    }, [])

    const handleOpen = (document) => {

        let data = {

            doctorName: doctors.find((doctor) => doctor.id === document.doctorId).firstName + " " + doctors.find((doctor) => doctor.id === document.doctorId).lastName,
            doctorPhone: doctors.find((doctor) => doctor.id === document.doctorId).phoneNumber,
            doctorEmail: doctors.find((doctor) => doctor.id === document.doctorId).email,
            doctorWard: doctors.find((doctor) => doctor.id === document.doctorId).ward,
            doctor: doctors.find((doctor) => doctor.id === document.doctorId),

            type: document.type,
            result: document.result,
            time: document.createdAt

        }
        setCurrentId(data)
        setShowModal(true)
    }

    return (
        documents.length > 0 && doctors.length > 0 ? [
            <>
                <table>
                    <thead>
                        <tr>
                            <th>Doctor</th>
                            <th>Exam</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {documents.map((document) => (
                            <tr key={document.id} >
                                <td>{doctors.find((doctor) => doctor.id === document.doctorId).firstName + " " + doctors.find((doctor) => doctor.id === document.doctorId).lastName}</td>
                                <td>{document.type}</td>
                                <td><Button variant="contained" color="success" onClick={() => handleOpen(document)}>Expand</Button></td>
                                <ModalDocument show={showModal} close={() => setShowModal(false)} data={currentId} />
                            </tr>
                        ))}
                    </tbody>

                </table>
            </>
        ] : [
            <div className="loading">Waiting for Data...</div>
        ]
    )
}

export default Documents