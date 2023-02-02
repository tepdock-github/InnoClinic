import AssignmentIndIcon from '@mui/icons-material/AssignmentInd';
import AssignmentIcon from '@mui/icons-material/Assignment';
import VaccinesIcon from '@mui/icons-material/Vaccines';
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts';
import SupervisedUserCircleIcon from '@mui/icons-material/SupervisedUserCircle';

export const NavbarItems = [
    {
        id: 0, 
        icon: <ManageAccountsIcon/>,
        label: "Authentication",
        route: "authentication"
    },
    {
        id: 1, 
        icon: <AssignmentIndIcon/>,
        label: "Appoitments",
        route: "appoitments"
    },
    {
        id: 2, 
        icon: <AssignmentIcon/>,
        label: "Medical Results",
        route: "medical-results"
    },
    {
        id: 3, 
        icon: <SupervisedUserCircleIcon/>,
        label: "Doctors",
        route: "doctors"
    },
    {
        id: 4, 
        icon: <VaccinesIcon/>,
        label: "Services",
        route: "services"
    }
]