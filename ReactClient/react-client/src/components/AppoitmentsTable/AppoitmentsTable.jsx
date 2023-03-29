import React, { useEffect, useState } from 'react'
import DataTable from '../common/DataTable/DataTable'

const columns = [
    {field: 'id', headerName: 'Id', width: 50},
    {field: 'doctorFirstName', headerName: 'Doctor first name', width: 150},
    {field: 'doctorLastName', headerName: 'Doctor last name', width: 150},
    {field: 'serviceName', headerName: 'Service Name', width: 150},
    {field: 'date', headerName: 'Date', width: 120},
    {field: 'time', headerName: 'Time', width: 120}
];

const appoitmentTableStyle = {
    height: '450px'
};

const AppoitmentsTable = ({onError}) => {
    const [appoitments, setAppoitments] = useState([]);

    useEffect(() => {
        const getAppoitments = async () => {
            const response = await fetch('https://localhost:7111/gateway/appoitments');
            setAppoitments(await response.json());
        }
        getAppoitments();
    }, []);

    return (
        <DataTable
            rows={appoitments}
            columns={columns}
            loading={!appoitments.length}
            sx={appoitmentTableStyle}
        />
    )
};

export default AppoitmentsTable;