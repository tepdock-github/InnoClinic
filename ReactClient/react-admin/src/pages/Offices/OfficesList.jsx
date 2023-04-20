import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import IconButton from '@mui/material/IconButton';
import RefreshIcon from '@mui/icons-material/Refresh';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import { Link } from 'react-router-dom';
import { cardHeaderStyles } from './styles';
import OfficesTable from "../../components/Office/OfficesTable";

const OfficesList = () => {
    const handleRefreshPage = () => {
        window.location.reload();
    };

    const getHeader = () => {
        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <Box>
                    <Link to={'/create/office'}>
                        <Button
                            variant='contained'
                            size='large'
                            sx={cardHeaderStyles.CreateAppoitmentButton}
                        >
                            Добавить офис
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
        <OfficesTable/>
    );

    return (
        <GridWrapper>
            <BasicCard
                header={getHeader()}
                content={getContent()}
            />
        </GridWrapper>
    )
};

export default OfficesList;