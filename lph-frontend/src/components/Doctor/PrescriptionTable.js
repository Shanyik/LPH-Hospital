const PresceptionTable = ({ presciptions }) => {
    return (
        <div id="prescriptionTableContainer">

            <table id="prescriptionTable">
                <thead>
                    <tr>
                        <th>Medication name</th>
                        <th>Created</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        presciptions.length >= 1 ? [presciptions.map((presciption)=>(
                            <tr>
                                <td>{presciption.name}</td>
                                <td>{presciption.createdAt}</td>
                            </tr>
                            ))] : [
                                <td colSpan="2">Prescreption not found </td>
                            ]
                    }
                    
                </tbody>
            </table>
        </div>
    )
}

export default PresceptionTable;