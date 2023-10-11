const ExaminationTable = ({examinations}) => {
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
                    {examinations.length == 0 ? 
                    [<td colSpan="2">Examination not found </td>] 
                    : 
                    [examinations.map((exam)=>(
                        <tr>
                        <td>{exam.type}</td>
                        <td>{exam.createdAt}</td>
                    </tr>))]
                    }
                </tbody>
            </table>
        </div>
    )

}

export default ExaminationTable;