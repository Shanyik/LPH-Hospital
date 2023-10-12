import * as React from 'react';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Button from '@mui/material/Button';
import { useEffect, useState } from 'react';


const getProduct = (id) => {
    return fetch(`/Product/GetById:${id}`).then(response => response.json())
}

const PrescreptionModal = ({ doctors, prescription, patients, handleClose, open }) => {

    const [product, setProduct] = useState(null);
    const [productLoaded, setProductLoaded] = useState(false);

    useEffect(() => {
        if (prescription !== undefined) {
            getProduct(prescription.productId).then(data => {
                setProduct(data);
                setProductLoaded(true);
                console.log(data);
            });
        }
    }, [prescription]);

    const style = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: 400,
        bgcolor: 'background.paper',
        border: '2px solid #000',
        boxShadow: 24,
        pt: 2,
        px: 4,
        pb: 3,
    };

    return (
        <Modal
            open={open}
            onClose={handleClose}
            aria-labelledby="parent-modal-title"
            aria-describedby="parent-modal-description"
        >
            {doctors && prescription && (
                <Box sx={{ ...style, width: 400 }}>
                    {
                        productLoaded ?
                        [
<>
                                <h2 id="parent-modal-title">Prescription {prescription.id} </h2>
                                <h4 id="parent-modal-description">Details:</h4>
                                <div>
                                    Doctor: {doctors.find((doctor) => doctor.Id === prescription.doctorID).firstName}{" "}
                                    {doctors.find((doctor) => doctor.Id === prescription.doctorID).lastName}
                                </div>

                                <div>
                                    Patient: {patients.find((patient) => patient.id === prescription.patientId).firstName}{" "}
                                    {patients.find((patient) => patient.id === prescription.patientId).lastName}
                                </div>

                                <div>
                                    Product: {product.name}
                                </div>

                                <div>
                                    Packing: {product.packing}
                                </div>
                                <h4 id="parent-modal-description">Description:</h4>
                                <div>
                                    {prescription.description}
                                </div>

                            </>

                        ] : [
                            <div>Loading..</div>
                        ]
                            
                        }
                </Box>
            )}
        </Modal>
    )

}

export default PrescreptionModal;