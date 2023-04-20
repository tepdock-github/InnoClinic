import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import IconButton from '@mui/material/IconButton';
import RefreshIcon from '@mui/icons-material/Refresh';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import { Link } from 'react-router-dom';
import AppoitmentsTable from '../../components/AppoitmentsTable/AppoitmentsTable';
import { cardHeaderStyles } from './styles';

const AppoitmentsList = () => {
    const handleRefreshPage = () => {
        window.location.reload();
    };


    const getHeader = () => {

        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <Box>
                    <Link to={'/create/appoitments'}>
                        <Button
                            variant='contained'
                            size='large'
                            sx={cardHeaderStyles.CreateAppoitmentButton}
                        >
                            Book Appoitment
                        </Button>
                    </Link>
                    <IconButton
                        onClick={handleRefreshPage} >
                        <RefreshIcon />
                    </IconButton>
                </Box>
                <Link to={'/history/appoitments'}>
                    <Button>
                        History
                    </Button>
                </Link>
            </Box>
        )
    };

    const getContent = () => (
        <AppoitmentsTable />
    );

    return (
        <GridWrapper>
            <BasicCard
                header={getHeader()}
                content={getContent()}
            />
        </GridWrapper>
    )
}

export default AppoitmentsList;