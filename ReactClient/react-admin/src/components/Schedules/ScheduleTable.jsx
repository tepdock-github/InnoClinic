import React from 'react';
import DoctorScheduleTable from './SchedulesTables/DoctorScheduleTable';
import ReceptionistScheduleTable from './SchedulesTables/ReceptionistScheduleTable';
import { useNavigate } from 'react-router-dom';

const ScheduleTable = () => {
    const navigate = useNavigate();

    var role = localStorage.getItem('role');

    if (role === 'Receptionist') return <ReceptionistScheduleTable/>
    else if (role === 'Doctor') return <DoctorScheduleTable/>
    else navigate('/401-error');
}

export default ScheduleTable;