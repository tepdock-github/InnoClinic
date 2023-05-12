import React from 'react';
import DoctorSpecializationList from './SpecializationsLists/DoctorSpecializationList';
import ReceptionistSpecializationList from './SpecializationsLists/ReceptionistSpecializationList';

const SpecializationList = () => {
    var role = localStorage.getItem('role');

    if (role === 'Receptionist') return <ReceptionistSpecializationList/>
    else return <DoctorSpecializationList/>
}

export default SpecializationList;