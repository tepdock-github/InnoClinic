import React from 'react';
import Badge from '@mui/material/Badge';
import NotificationsNoneIcon from '@mui/icons-material/NotificationsNone';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import BasicMenu from '../common/Menu/BasicMenu';

const notifications = [
    {
        id: 0,
        label: 'First notification'
    },
    {
        id: 1,
        label: 'Second notification'
    },
];

const NotificarionBell = ({ iconColor }) => {

    const [open, setOpen] = React.useState(false);
    const [anchorEl, setAnchorEl] = React.useState(null);

    const newNotifs = `you have ${notifications.length} notifications`;
    const noNotifs = "you dont have new notifications";

    const handleOpen = (event) => {
        setAnchorEl(event.currentTarget)
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <>
            <Tooltip title={notifications.length ? newNotifs : noNotifs}>
                <IconButton
                    color={iconColor}
                    onClick={notifications.length ? handleOpen : null}
                    anchorEl={anchorEl}>
                    <Badge
                        badgeContent={notifications.length}
                        color="error"
                    >
                        <NotificationsNoneIcon />
                    </Badge>
                </IconButton>
            </Tooltip>
            <BasicMenu
                open={open}
                anchorEl={anchorEl}
                handleClose={handleClose}
                menuItems={notifications}
            />
        </>
    )
}

export default NotificarionBell;