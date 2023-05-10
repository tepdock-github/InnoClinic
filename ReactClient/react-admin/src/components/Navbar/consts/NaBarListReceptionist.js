import AssignmentIndIcon from '@mui/icons-material/AssignmentInd';
import VaccinesIcon from '@mui/icons-material/Vaccines';
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts';
import SupervisedUserCircleIcon from '@mui/icons-material/SupervisedUserCircle';
import AddHomeWorkIcon from '@mui/icons-material/AddHomeWork';
import BiotechIcon from '@mui/icons-material/Biotech';
import ChromeReaderModeIcon from '@mui/icons-material/ChromeReaderMode';
import ContactPageIcon from '@mui/icons-material/ContactPage';
import WorkHistoryIcon from '@mui/icons-material/WorkHistory';

export const NavbarItemsReceptionist = [
    {
        id: 0, 
        icon: <ManageAccountsIcon/>,
        label: "Ваш профиль",
        route: "profile"
    },
    {
        id: 1, 
        icon: <ContactPageIcon/>,
        label: "Учётные записи",
        route: "accounts"
    },
    {
        id: 2, 
        icon: <AssignmentIndIcon/>,
        label: "Записи на приём",
        route: "appoitments"
    },
    {
        id: 3, 
        icon: <SupervisedUserCircleIcon/>,
        label: "Врачи",
        route: "doctors"
    },
    {
        id: 4, 
        icon: <ChromeReaderModeIcon/>,
        label: "Регистратура",
        route: "receptionist"
    },
    {
        id: 5, 
        icon: <VaccinesIcon/>,
        label: "Сервисы",
        route: "services"
    },
    {
        id: 6, 
        icon: <BiotechIcon/>,
        label: "Специализации",
        route: "specializations"
    },
    {
        id: 7, 
        icon: <AddHomeWorkIcon/>,
        label: "Офисы",
        route: "offices"
    },
    {
        id: 8,
        icon: <WorkHistoryIcon/>,
        label: "Расписания",
        route: "schedules"
    }
]