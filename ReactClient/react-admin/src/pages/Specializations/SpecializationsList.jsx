import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import { Link } from 'react-router-dom';
import { cardHeaderStyles } from './styles';
import SpecializationTable from '../../components/SpecializationTable/SpecializationTable';

const SpecializationList = () => {
    const getHeader = () => {

        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <Box>
                    <Link to={'/create/specializations'}>
                        <Button
                            variant='contained'
                            size='large'
                            sx={cardHeaderStyles.CreateAppoitmentButton}
                        >
                            Создание специализации
                        </Button>
                    </Link>
                </Box>
            </Box>
        )
    };

    const getContent = () => (
        <SpecializationTable />
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

export default SpecializationList;