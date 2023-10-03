import React, {useState} from 'react';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';
import HelpIcon from '@mui/icons-material/Help';
import Box from '@mui/material/Box';
import SignUpModal from '../../components/Modals/SignUpModal';
import SignInModal from '../../components/Modals/SignInModal';
import Button from '@mui/material/Button';
import { headerStyles } from './styles';

const Header = ({ title }) => {
    const [openSignUp, setOpenSignUp] = useState(false); 
    const [openSignIn, setOpenSignIn] = useState(false);
    const handleOpenSignUp = () => {
        setOpenSignUp(true);
    }
    const handleCloseSignUp = () => {
        setOpenSignUp(false);
    }

    const handleOpenSignIn = () => {
        setOpenSignIn(true);
    }
    const handleCloseSignIn = () => {
        setOpenSignIn(false);
    }

    return (
        <Box sx={headerStyles.wrapper}>
            <Box sx={headerStyles.middleRow}>
                <Typography
                    variant='h5'
                    color='white'>
                    {title}
                </Typography>
                <Box>
                    <Button
                        sx={headerStyles.logotButton}
                        variant="contained"
                        onClick={handleOpenSignUp}
                        >
                            Sign Up
                    </Button>
                    <SignUpModal isOpen={openSignUp} onClose={()=>handleCloseSignUp(false)} />
                    <Button
                        sx={headerStyles.logotButton}
                        variant="contained"
                        onClick={handleOpenSignIn}
                        >
                            Sign In
                    </Button>
                    <SignInModal isOpen={openSignIn} onClose={()=>handleCloseSignIn(false)}/>
                    <Tooltip title="help">
                        <IconButton
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