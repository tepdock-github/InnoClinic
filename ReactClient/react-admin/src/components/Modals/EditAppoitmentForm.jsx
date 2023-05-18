import React from 'react';
import DoctorEditAppoitmentForm from './EditAppoitmentsForms/DoctorEditAppoitment';
import ReceptionistEditAppoitmentForm from './EditAppoitmentsForms/ReceptionistEditAppoitment';


const EditAppoitmentForm = () => {
    var role = localStorage.getItem('role');

    if(role === 'Receptionist') return <ReceptionistEditAppoitmentForm/>
    else return <DoctorEditAppoitmentForm/>
    
}

export default EditAppoitmentForm;