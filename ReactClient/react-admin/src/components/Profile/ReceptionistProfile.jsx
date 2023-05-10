import React from 'react';
import { useParams } from "react-router-dom";
import { useEffect, useState } from 'react'
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import GridWrapper from '../common/GridWrapper/GridWrapper';
import BackgroundLetterAvatars from '../common/Avatar/Avatar';
import { Link } from 'react-router-dom';

const ReceptionistDetailsModal = () => {
    const { id } = useParams();
    const [recep, setRecep] = useState([]);
    const [office, setOffices] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchDoctor = async () => {
            try {
                const responseDoctor = await fetch(`http://localhost:7111/gateway/receptionists/${id}`, {headers});
                if (!responseDoctor.ok) {
                    throw new Error(responseDoctor.statusText);
                }
                setRecep(await responseDoctor.json());
                const responseOffice = await fetch(`http://localhost:7111/gateway/offices/${recep.officeId}`);
                if (!responseOffice.ok) {
                    throw new Error(responseOffice.statusText);
                }
                setOffices(await responseOffice.json());
            } catch (error) {
                console.error(error);
            }
        };
        fetchDoctor();
    }, []);

    return (
        <GridWrapper>
            <Card sx={{ maxWidth: 500 }}>
                <CardMedia sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: 75 }}>
                    <BackgroundLetterAvatars
                        firstName={recep.firstName}
                        lastName={recep.lastName}
                    />
                </CardMedia>
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {recep.firstName} {recep.middleName} {recep.lastName}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        работает в больнице по адресу: {office.address}
                    </Typography>
                </CardContent>
                <CardActions>
                    <Link to={'/receptionist'}>
                        <Button size='small' variant='outlined'>Go back</Button>
                    </Link>
                </CardActions>
            </Card>
        </GridWrapper>
    );
}

export default ReceptionistDetailsModal;