import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import { Link } from 'react-router-dom';
import AppoitmentsHistoryTables from '../../components/AppoitmentsTable/AppoitmentsHistoryTable';
import { cardHeaderStyles } from './styles';

const AppoitmentsHistoryList = () => {
    const getHeader = () => {
        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <Box>
                    <Link to={'/appoitments'}>
                        <Button
                            variant='contained'
                            size='large'
                            sx={cardHeaderStyles.CreateAppoitmentButton}
                        >
                            Go back
                        </Button>
                    </Link>
                </Box>
            </Box>
        )
    };

    const getContent = () => (
        <AppoitmentsHistoryTables/>
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

export default AppoitmentsHistoryList