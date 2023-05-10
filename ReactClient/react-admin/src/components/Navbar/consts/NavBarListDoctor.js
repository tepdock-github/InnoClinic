import AssignmentIndIcon from '@mui/icons-material/AssignmentInd';
import AssignmentIcon from '@mui/icons-material/Assignment';
import VaccinesIcon from '@mui/icons-material/Vaccines';
import ManageAccountsIcon from '@mui/icons-material/ManageAccounts';
import BiotechIcon from '@mui/icons-material/Biotech';
import ChromeReaderModeIcon from '@mui/icons-material/ChromeReaderMode';
import WorkHistoryIcon from '@mui/icons-material/WorkHistory';

export const NavbarItemsDoctor = [
    {
        id: 0, 
        icon: <ManageAccountsIcon/>,
        label: "Ваш профиль",
        route: "profile"
    },
    {
        id: 1, 
        icon: <AssignmentIndIcon/>,
        label: "Записи на приём",
        route: "appoitments"
    },
    {
        id: 3, 
        icon: <AssignmentIcon/>,
        label: "Результаты приёма",
        route: "results"
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
        icon: <WorkHistoryIcon/>,
        label: "Расписание",
        route: "schedules"
    }
]