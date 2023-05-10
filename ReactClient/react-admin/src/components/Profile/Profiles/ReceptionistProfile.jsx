import React from 'react';
import { useEffect, useState } from 'react'
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import GridWrapper from '../../common/GridWrapper/GridWrapper';
import BackgroundLetterAvatars from '../../common/Avatar/Avatar';
import { Link, useNavigate } from 'react-router-dom';

const ReceptionistProfile = () => {
    const navigate = useNavigate();
    const [data, setData] = useState([]);
    const [office, setOffices] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            const response = await fetch(`http://localhost:7111/gateway/receptionists/account/${userId}`, {
                headers: headers
            });
            if (response.status === 401) {
                navigate('/401-error');
            }
            else if (response.status === 403) {
                navigate('/403-error')
            }
            else if (response.status === 404) {
                navigate('/profile/receptionist/create');
            }
            const data = await response.json();
            setData(data)

            const responseOffice = await fetch(`http://localhost:7111/gateway/offices/${data.officeId}`);
            setOffices(await responseOffice.json());
        };
        fetchData();
    }, [data.officeId]);
    

    return (
        <GridWrapper>
            <Card sx={{ maxWidth: 500 }}>
                <CardMedia sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: 75 }}>
                    <BackgroundLetterAvatars
                        firstName={data.firstName}
                        lastName={data.lastName}
                    />
                </CardMedia>
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {data.firstName} {data.middleName} {data.lastName}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        работает в больнице по адресу: {office.address}
                    </Typography>
                </CardContent>
                <CardActions>
                    <Link to={'/profile/edit/receptionist'}>
                        <Button size='small' variant='outlined'>Изменить</Button>
                    </Link>
                </CardActions>
            </Card>
        </GridWrapper>
    );
}

export default ReceptionistProfile;