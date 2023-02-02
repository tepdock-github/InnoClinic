import React from "react";
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import SearchBar from '../../components/common/SearchBar/SearchBar';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { cardHeaderStyles } from './styles';
import Filter from '../../components/common/Filter/Filter';
import Button from '@mui/material/Button';

const specializations = [
    {
        id: 0,
        title: 'aboba'
    },
    {
        id: 1,
        title: 'obabo'
    }
]
const offices = [
    {
        id: 0,
        title: 'Gukova street 29'
    },
    {
        id: 1,
        title: 'Lebno 345'
    },
    {
        id: 2,
        title: '1tyt'
    }
]

const Doctors = () => {
    const getHeader = () => {
        const handleChange = (value) => console.log(value);

        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <SearchBar
                    placeholder='search doctors by name'
                    onChange={(event) => handleChange(event.target.value)}
                    searchBarWidth='630px'
                />
                <Filter items={specializations} label={"specialization"}/>
                <Filter items={offices} label={"office"}/>
                <Button>
                    Apply
                </Button>
            </Box>
        )
    }

    const getContent = () => (
        <Typography
            align='center'
            sx={{ margin: '40px 16px', color: 'rgba(0, 0, 0, 0.6)', fontSize: '1.3rem' }}>
            No Doctors yet
        </Typography>
    )

    return (
        <GridWrapper>
            <BasicCard
                header={getHeader()}
                content={getContent()}
            />
        </GridWrapper>
    )
}

export default Doctors;