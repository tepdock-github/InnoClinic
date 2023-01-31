import React, { useState } from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import NewAppoitmentModal from '../../components/Modals/NewAppoitmentModal';
import IconButton from '@mui/material/IconButton';
import RefreshIcon from '@mui/icons-material/Refresh';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { cardHeaderStyles } from './styles';

const AppoitmentsList = () => {
    const [open, setOpen] = useState(false); 

    const getHeader = () => {

        const addAppoitment = () => setOpen(true);

        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <Box>
                    <Button
                        variant='contained'
                        onClick={addAppoitment}
                        size='large'
                        sx={cardHeaderStyles.CreateAppoitmentButton}
                    >
                        Book Appoitment
                    </Button>
                    <IconButton>
                        <RefreshIcon />
                    </IconButton>
                </Box>
                <Button>
                    History
                </Button>
            </Box>
        )
    };

    const getContent = () => (
        <Typography
            align='center'
            sx={{ margin: '40px 16px', color: 'rgba(0, 0, 0, 0.6)', fontSize: '1.3rem' }}
        >
            No appoitments
        </Typography>
    );

    return (
        <GridWrapper>
            <BasicCard
                header={getHeader()}
                content={getContent()}
            />
            <NewAppoitmentModal open={open} onClose={()=>setOpen(false)} />
        </GridWrapper>
    )
}

export default AppoitmentsList;