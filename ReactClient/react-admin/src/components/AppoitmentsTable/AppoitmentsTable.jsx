import React from 'react';
import AppoitmentsTableDoctor from './AppoitmentsTables/AppoitmentsTableDoctor';
import AppoitmentsTableReceptionist from './AppoitmentsTables/AppoitmentsTableReceptionist';

const AppoitmentsTable = () => {
    var role = localStorage.getItem('role');

    return (
        <>
            {role === 'Doctor' && <AppoitmentsTableDoctor/>}
            {role === 'Receptionist' && <AppoitmentsTableReceptionist/>}
        </>
    )
};

export default AppoitmentsTable;