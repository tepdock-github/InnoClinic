import * as React from 'react';
import NavBarDoctor from './NavBars/NavBarDoctor';
import NavBarReceptionist from './NavBars/NavBarReceptionist';

const Navbar = () => {
    var role = localStorage.getItem('role');

    return (
        <>
            {role === 'Doctor' && <NavBarDoctor/>}
            {role === 'Receptionist' && <NavBarReceptionist/>}
        </>
    );
}

export default Navbar;