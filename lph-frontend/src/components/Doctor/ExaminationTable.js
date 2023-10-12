import { useEffect, useState } from "react";
import ExamModal from "./ExamModal";

const ExaminationTable = ({ examinations, patients, doctor }) => {

    const [open, setOpen] = useState(false);
    const [render, setRender] = useState(false);
    const [currentExam, setCurrenExam] = useState(null)

    const handleClose = () => {
        setOpen(false);

    };

    const handleOpen = (exam) => {
        setCurrenExam(exam)
        setOpen(true)

    };

    useEffect(() => {
        console.log(patients)
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
                    {examinations.length === 0 ?
                        [<td colSpan="2">Examination not found </td>]
                        :
                        [examinations.map((exam) => (
                            <tr onClick={() => (handleOpen(exam))}>
                                <td>{exam.type}</td>
                                <td>{new Date(exam.createdAt).toLocaleDateString()}{" "}
                                    {new Date(exam.createdAt).toLocaleTimeString()}
                                </td>
                            </tr>))]
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