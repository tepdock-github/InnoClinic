import React from 'react';
import DoctorResultTable from './ResultTables/DoctorResultTable';
import ReceptionistResultTable from './ResultTables/ReceptionistResultTable';


const ResultsTable = () => {
    var role = localStorage.getItem('role');

    return (
        <>
            {role === 'Doctor' && <DoctorResultTable/>}
            {role === 'Receptionist' && <ReceptionistResultTable/>}
        </>
    )
};

export default ResultsTable;