import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import IconButton from '@mui/material/IconButton';
import RefreshIcon from '@mui/icons-material/Refresh';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import { Link } from 'react-router-dom';
import { cardHeaderStyles } from './styles';
import ScheduleTable from '../../components/Schedules/ScheduleTable';

const ScheduleList = () => {
    const handleRefreshPage = () => {
        window.location.reload();
    };


    const getHeader = () => {

        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <Box>
                    <Link to={'/create/schedules'}>
                        <Button
                            variant='contained'
                            size='large'
                            sx={cardHeaderStyles.CreateAppoitmentButton}
                        >
                            Создать слот
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
        <ScheduleTable />
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

export default ScheduleList;