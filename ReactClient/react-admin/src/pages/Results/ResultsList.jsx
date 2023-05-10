import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import { cardHeaderStyles } from './styles';
import { Link } from 'react-router-dom';
import ResultsTable from '../../components/ResultsTable/ResultsTable';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import RefreshIcon from '@mui/icons-material/Refresh';

const ResultsList = () => {
    const handleRefreshPage = () => {
        window.location.reload();
    };

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
                            To appoitments
                        </Button>
                    </Link>
                    <IconButton
                        onClick={handleRefreshPage} >
                        <RefreshIcon />
                    </IconButton>
                </Box>
            </Box>
        )
    };

    const getContent = () => (
        <ResultsTable/>
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

export default ResultsList;