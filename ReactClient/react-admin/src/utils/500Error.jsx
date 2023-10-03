import React from 'react';
import Card from '@mui/material/Card';
import Typography from '@mui/material/Typography';
import CardContent from '@mui/material/CardContent';
import GridWrapper from '../components/common/GridWrapper/GridWrapper';

const Error500 = () => {
  return (
    <GridWrapper>
        <Card sx={{ maxWidth: 750, minWidth: 550 }}>
            <CardContent sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <Typography variant='h1' component='div' >
                    500 Ошибка
                </Typography>
                <Typography>
                    Что-то пошло не так. Пожалуйста попробуйте позже.
                </Typography>
            </CardContent>
        </Card>
    </GridWrapper>
  );
};

export default Error500;
