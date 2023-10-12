import React, { useEffect, useState } from 'react'
import ModalPrescription from './ModalPrescription';


const Prescriptions = (props) => {

    const [presciptions, setPresciptions] = useState([])
    const [doctors, setDoctors] = useState([])
    const [products, setProducts] = useState([])
    const [showModal, setShowModal] = useState(false)
    const [currentId, setCurrentId] = useState(false)
    
    useEffect(() => {
        fetch(`/Prescription/GetByPatientId:${props.userId}`)
            .then(response => response.json())
            .then(data => {
                setPresciptions(data);
                console.log(data);
            }
            )
            .catch(error => console.log(error))
        fetch(`/Doctor/GetAll`)
            .then(response => response.json())
            .then(data => {
                setDoctors(data);
                console.log(data);
            }
            )
            .catch(error => console.log(error))
        fetch(`/Product/GetAll`)
            .then(response => response.json())
            .then(data => {
                setProducts(data);
                console.log(data);
            }
            )
            .catch(error => console.log(error))
    }, [props.userId])
    
    const handleOpen = (presciption) => {

        let data = {
            productName: products.find(product => product.id === presciption.productId).name,
            productPacking: products.find(product => product.id === presciption.productId).packing,
            productSubsitutable: products.find(product => product.id === presciption.productId).subsitutable,

            doctorName: doctors.find((doctor) => doctor.id === presciption.doctorId).firstName + " " + doctors.find((doctor) => doctor.id === presciption.doctorId).lastName,
            doctorPhone: doctors.find((doctor) => doctor.id === presciption.doctorId).phoneNumber,
            doctorEmail: doctors.find((doctor) => doctor.id === presciption.doctorId).email,
            doctorWard: doctors.find((doctor) => doctor.id === presciption.doctorId).ward,
            doctor: doctors.find((doctor) => doctor.id === presciption.doctorId),

            description: presciption.description,
            dispensed: presciption.createdAt

        }
        setCurrentId(data)
        setShowModal(true)
    }

  return (
    presciptions.length > 0 && doctors.length > 0 && products.length > 0 ? [
        <>
        <table>
            <thead>
                <tr>
                    <th>Doctor</th>
                    <th>Product</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {presciptions.map((presciption) => (
                    <tr key={presciption.id} >
                        <td>{doctors.find((doctor) => doctor.id === presciption.doctorId).firstName + " " + doctors.find((doctor) => doctor.id === presciption.doctorId).lastName }</td>
                        <td>{products.find(product => product.id === presciption.productId).name}</td>
                        <td><button onClick={() => handleOpen(presciption, products, doctors)}>Expand</button></td>
                        <ModalPrescription show={showModal} close={() => setShowModal(false)} data={currentId}/>
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

export default Prescriptions