import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import SearchBar from '../../components/common/SearchBar/SearchBar';
import IconButton from '@mui/material/IconButton';
import RefreshIcon from '@mui/icons-material/Refresh';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { cardHeaderStyles } from './styles';

const AppoitmentsList = () => {
    const getHeader = () => {

        const addAppoitment = () => console.log('placeholder click');
        const handleChange = (value) => console.log(value);

        return (
            <Box sx={cardHeaderStyles.wrapper}>
                <SearchBar
                    placeholder="search"
                    onChange={(event) => handleChange(event.target.value)}
                    searchBarWidth='720px'
                />
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
        </GridWrapper>
    )
}

export default AppoitmentsList;