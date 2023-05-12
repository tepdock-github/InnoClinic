import React from 'react';
import DoctorServicesTable from './ServiceTables/DoctorServicesTable';
import ReceptionistServicesTable from './ServiceTables/ReceptionistServicesTable';


const ServiceTable = () => {
    var role = localStorage.getItem('role');

    if (role === 'Receptionist') return <ReceptionistServicesTable />
    else return <DoctorServicesTable />
};

export default ServiceTable;