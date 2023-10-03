import React from 'react';
import DoctorSpecializationTable from './SpecializationTables/DoctorSpecializationTable';
import ReceptionistSpecializationTable from './SpecializationTables/ReceptionistSpecializationTable';

const SpecializationTable = () => {
    var role = localStorage.getItem('role');

    if (role === 'Receptionist') return <ReceptionistSpecializationTable />
    else return <DoctorSpecializationTable />
}

export default SpecializationTable;