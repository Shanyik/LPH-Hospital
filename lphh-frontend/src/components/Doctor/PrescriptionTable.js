import * as React from 'react';
import { useState } from "react";
import PrescreptionModal from './PrescriptionModal';



const PresceptionTable = ({ presciptions, doctors, patients, cookie }) => {

    const [open, setOpen] = useState(false);
    const [currentpresception, setPresception] = useState([]);

    
    const handleClose = () => {
        setOpen(false);

    };

    const handleOpen = (pres) => {
        setPresception(pres)
        setOpen(true)

    };


    return (
        <div id="prescriptionTableContainer">
            {
                doctors !== undefined ?
                    [
                        <table id="prescriptionTable">
                            <thead>
                                <tr>
                                    <th>Doctor</th>
                                    <th>Created</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    presciptions.length >= 1 ? [presciptions.map((presciption) => (
                                        <>
                                            <tr onClick={() => (handleOpen(presciption))}>
                                                <td>{doctors.find((doctor) => doctor.Id === presciption.doctorID).firstName}{" "}
                                                    {doctors.find((doctor) => doctor.Id === presciption.doctorID).lastName}
                                                </td>
                                                <td>{new Date(presciption.createdAt).toLocaleDateString()}{" "}
                                                    {new Date(presciption.createdAt).toLocaleTimeString()}</td>
                                            </tr>

                                        </>

                                    ))] : [
                                        <td colSpan="2">Prescreption not found </td>
                                    ]
                                }

                            </tbody>
                        </table>
                    ] : [
                        <div>Loading...</div>
                    ]
                
            }

            {
                open === true ? [
                    <PrescreptionModal doctors={doctors} prescription={currentpresception}
                        patients={patients} handleClose={handleClose} open={open} cookie={cookie} />
                ] : [

                ]
            }
        </div>
    )
}

export default PresceptionTable;