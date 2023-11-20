import { useEffect, useState } from "react";
import ExamModal from "./ExamModal";

const ExaminationTable = ({ examinations, patients, doctor }) => {

    const [open, setOpen] = useState(false);
    const [currentExam, setCurrenExam] = useState(null)

    const handleClose = () => {
        setOpen(false);

    };

    const handleOpen = (exam) => {
        setCurrenExam(exam)
        setOpen(true)
       
    };

    useEffect(() => {
        console.log(examinations)
        console.log(doctor)
    });

    return (

        <div id="examTableContainer">
            <table id="examTable">
                <thead>
                    <tr>
                        <th>Examination</th>
                        <th>Created</th>
                    </tr>
                </thead>
                <tbody>
                    {examinations.length >= 1 ?
                        [
                            console.log(examinations.length),
                            examinations.map((exam) => (
                            <tr onClick={() => (handleOpen(exam))}>
                                <td>{exam.type}</td>
                                <td>{new Date(exam.createdAt).toLocaleDateString()}{" "}
                                    {new Date(exam.createdAt).toLocaleTimeString()}
                                </td>
                            </tr>))

                        
                        ]
                        :
                        [
                            <td colSpan="2">Examination not found </td>
                           ]
                    }
                </tbody>
            </table>
            {
                open === true ? [
                    <ExamModal handleClose={handleClose} open={open} exam={currentExam} patients={patients} doctors={doctor} />
                ] : [

                ]
            }
        </div>


    )

}

export default ExaminationTable;