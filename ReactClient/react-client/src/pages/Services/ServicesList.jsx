import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import SearchBar from '../../components/common/SearchBar/SearchBar';
import Box from '@mui/material/Box';
import { cardHeaderStyles } from './styles';
import Typography from '@mui/material/Typography';

const ServicesList = () => {
    const getHeader = () => {
        const handleChange = (value) => {
            console.log(value);
        };

        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <SearchBar
                    placeholder="placeholder"
                    onChange={(event) => handleChange(event.target.value)}
                    searchBarWidth='720px'
                />
            </Box>
        )
    };

    const getContent = () => (
        <Typography
            align="center"
            sx={{ margin: '40px 16px', color: 'rgba(0, 0, 0, 0.6)', fontSize: '1.3rem' }}>
            No services here yet
        </Typography>
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

export default ServicesList;