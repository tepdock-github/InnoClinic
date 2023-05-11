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
import { Link, useParams } from 'react-router-dom';

const ReceptionistDetails = () => {
    const { id } = useParams();
    const [admin, setAdmin] = useState([]);
    const [office, setOffices] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            const responseAdmin = await fetch(`http://localhost:7111/gateway/receptionists/${id}`, {
                headers: headers
            });
            const admin = await responseAdmin.json()
            setAdmin(admin);

            if (admin.officeId) {
                const responseOffice = await fetch(`http://localhost:7111/gateway/offices/${admin.officeId}`);
                setOffices(await responseOffice.json());
            }
        };
        fetchData();
    }, []);

    return (
        <GridWrapper>
            <Card sx={{ maxWidth: 500 }}>
                <CardMedia sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: 75 }}>
                    <BackgroundLetterAvatars
                        firstName={admin.firstName}
                        lastName={admin.lastName}
                    />
                </CardMedia>
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {admin.firstName} {admin.middleName} {admin.lastName}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        работает в больнице по адресу: {office.address}
                    </Typography>
                </CardContent>
                <CardActions>
                    <Link to={'/receptionist'}>
                        <Button size='small' variant='outlined'>Назад</Button>
                    </Link>
                </CardActions>
            </Card>
        </GridWrapper>
    );
}

export default ReceptionistDetails;