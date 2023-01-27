import React from 'react';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';
import HelpIcon from '@mui/icons-material/Help';
import Box from '@mui/material/Box';
import BackgroundLetterAvatars from '../common/Avatar/Avatar';
import NotificarionBell from '../Notification/NotificarionBell';
import Button from '@mui/material/Button';

const Header = ({ title }) => {
    const headerStyles = {
        wrapper: {
            width: '100%',
            display: 'flex',
            flexDirection: 'column',
            backgroundColor: '#2176FF',
            padding: '10px',
        },
        topRow: {
            display: 'flex',
            flexDirection: 'row',
            justifyContent: 'end',
            alignItems: 'center',
            marginBottom: '20px',
            '*': {
                marginRight: '5px',
            },
        },
        middleRow: {
            display: 'flex',
            flexDirection: 'row',
            alignItems: 'center',
            justifyContent: 'space-between',
            marginBottom: '20px',
            marginLeft: '320px',
        },
        logotButton: {
            marginRight: '5px',
            color: 'white'
        },
    };

    return (
        <Box sx={headerStyles.wrapper}>
            <Box sx={headerStyles.topRow}>
                <NotificarionBell iconColor={"white"} />
                <BackgroundLetterAvatars firstName={"K"} lastName={"K"} />
            </Box>
            <Box sx={headerStyles.middleRow}>
                <Typography
                    variant='h5'
                    color='white'>
                    {title}
                </Typography>
                <Box>
                    <Button
                        sx={headerStyles.logotButton}
                        variant="outlined">Logout</Button>
                    <Tooltip title="help">
                        <IconButton
                            color="white"
                            sx={headerStyles.helpIcon}
                        >
                            <HelpIcon />
                        </IconButton>
                    </Tooltip>
                </Box>
            </Box>
        </Box>
    )
}

export default Header;