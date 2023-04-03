import React, { useEffect, useState } from 'react'
import DataTable from '../common/DataTable/DataTable'

const columns = [
    {field: 'firstName', headerName: 'First name', width: 150},
    {field: 'lastName', headerName: 'Last name', width: 150},
    {field: 'specializationName', headerName: 'Specialization name', width: 150}
];

const serviceTableStyle = {
    height: '450px'
};

const DoctorsTable = ({onError}) => {
    const [doctors, setDoctors] = useState([]);

    useEffect(() => {
        const getDoctors = async () => {
            const response = await fetch('http://localhost:7111/gateway/doctors');
            setDoctors(await response.json());
        }
        getDoctors();
    }, []);

    return (
        <DataTable 
            rows={doctors}
            columns={columns}
            loading={!doctors.length}
            sx={serviceTableStyle}
        />
    );
};

export default DoctorsTable;