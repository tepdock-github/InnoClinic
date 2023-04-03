import React from "react";
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import SearchBar from '../../components/common/SearchBar/SearchBar';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { cardHeaderStyles } from './styles';
import Filter from '../../components/common/Filter/Filter';
import Button from '@mui/material/Button';
import DoctorsTable from "../../components/DoctorsTable/DoctorsTable";

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
                {/* <Filter items={specializations} label={"specialization"}/>
                <Filter items={offices} label={"office"}/> */}
                <Button>
                    Apply
                </Button>
            </Box>
        )
    }

    const getContent = () => (
        <DoctorsTable/>
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