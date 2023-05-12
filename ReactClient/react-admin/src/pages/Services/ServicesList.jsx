import React from 'react';
import DoctorServiceList from './ServicesLists/DoctorServiceList';
import ReceptionistServiceList from './ServicesLists/ReceptionistServiceList';

const ServicesList = () => {
    var role = localStorage.getItem('role');

    if (role === 'Receptionist') return <ReceptionistServiceList/>
    else return <DoctorServiceList/>
}

export default ServicesList;