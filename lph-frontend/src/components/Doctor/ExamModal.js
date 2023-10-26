import * as React from 'react';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Button from '@mui/material/Button';
import { useEffect, useState } from 'react';

const ExamModal = ({open, handleClose, exam, patients, doctors}) => {

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

    useEffect(() => {console.log(doctors)})

    return (
        <Modal
            open={open}
            onClose={handleClose}
            aria-labelledby="parent-modal-title"
            aria-describedby="parent-modal-description"
        >

            <Box sx={{ ...style, width: 400 }}>

                <h2 id="parent-modal-title">{exam.type}</h2>
                <h4 id="parent-modal-description">Details:</h4>
                <div>
                    Doctor: {doctors.find((doctors)=>doctors.id === exam.doctorId).firstName}{" "}
                                {doctors.find((doctors)=>doctors.id === exam.doctorId).lastName}
                </div>
                <div>
                    Patient: {patients.find((patient)=>patient.id === exam.patientId).firstName}{" "}
                                {patients.find((patient)=>patient.id === exam.patientId).lastName}
                </div>
                <h4>
                    Result:
                </h4>
                <div>
                    {exam.result}
                </div>
            </Box>

        </Modal>
    )

}

export default ExamModal;