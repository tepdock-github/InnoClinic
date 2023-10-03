import React from 'react';
import Card from '@mui/material/Card';
import Typography from '@mui/material/Typography';
import CardContent from '@mui/material/CardContent';
import GridWrapper from '../components/common/GridWrapper/GridWrapper';

const Error403 = () => {
  return (
    <GridWrapper>
        <Card sx={{ maxWidth: 750, minWidth: 550 }}>
            <CardContent sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                <Typography variant='h1' component='div' >
                    403 Ошибка
                </Typography>
                <Typography>
                    У вас не хватает прав для просмотра данной страницы.
                </Typography>
            </CardContent>
        </Card>
    </GridWrapper>
  );
};

export default Error403;
