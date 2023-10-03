import React from 'react';
import DoctorProfile from './Profiles/DoctorProfile';
import ReceptionistProfile from './Profiles/ReceptionistProfile';

const ProfileCard = () => {
    var role = localStorage.getItem('role');

    return (
        <>
            {role === 'Doctor' && <DoctorProfile/>}
            {role === 'Receptionist' && <ReceptionistProfile/>}
        </>
    )
}

export default ProfileCard;