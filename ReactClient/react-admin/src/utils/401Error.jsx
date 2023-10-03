import React from 'react';
import Card from '@mui/material/Card';
import Typography from '@mui/material/Typography';
import CardContent from '@mui/material/CardContent';
import GridWrapper from '../components/common/GridWrapper/GridWrapper';

const Error401 = () => {
  return (
    <GridWrapper>
        <Card sx={{ maxWidth: 750, minWidth: 550 }}>
            <CardContent sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <Typography variant='h1' component='div' >
                    401 Ошибка
                </Typography>
                <Typography gutterBottom>
                    Пожалуйста авторизуйтесь или зарегистрируйтесь для просмотра данной страницы.
                </Typography>
            </CardContent>
        </Card>
    </GridWrapper>
  );
};

export default Error401;
