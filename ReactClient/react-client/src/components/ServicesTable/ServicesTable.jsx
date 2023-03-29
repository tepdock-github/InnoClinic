import React, { useEffect, useState } from 'react'
import DataTable from '../common/DataTable/DataTable'

const columns = [
    {field: 'serviceName', headerName: 'Service Name', width: 150},
    {field: 'price', headerName: 'Price', width: 50},
    {field: 'serviceCategory', headerName: 'Service Category', width: 150}
];

const serviceTableStyle = {
    height: '450px'
};

const ServiceTable = ({onError}) => {
    const [services, setServices] = useState([]);

    useEffect(() => {
        const getServices = async () => {
            const response = await fetch('https://localhost:7196/api/services');
            setServices(await response.json());
        }
        getServices();
    }, []);

    return (
        <DataTable 
            rows={services}
            columns={columns}
            loading={!services.length}
            sx={serviceTableStyle}
        />
    );
};

export default ServiceTable;