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
import { Link, useNavigate } from 'react-router-dom';

const DoctorDetailsModal = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [doctor, setDoctor] = useState([]);
    const [office, setOffices] = useState([]);

    useEffect(() => {
        const fetchDoctor = async () => {
            try {
                const responseDoctor = await fetch(`http://localhost:7111/gateway/doctors/${id}`);
                if (responseDoctor.status === 401) {
                    navigate('/401-error');
                }
                else if (responseDoctor.status === 403) {
                    navigate('/403-error')
                }
                else navigate('/500-error')
                setDoctor(await responseDoctor.json());

                const responseOffice = await fetch(`http://localhost:7111/gateway/offices/${doctor.officeId}`);
                if (responseOffice.status === 401) {
                    navigate('/401-error');
                }
                else if (responseOffice.status === 403) {
                    navigate('/403-error')
                }
                else navigate('/500-error')
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
                        firstName={doctor.firstName}
                        lastName={doctor.lastName}
                    />
                </CardMedia>
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {doctor.firstName} {doctor.middleName} {doctor.lastName}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {doctor.specializationName} с опытом работы: {doctor.careerStartYear} - текущий год
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        статус: {doctor.status}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        работает в больнице по адресу: {office.address}
                    </Typography>
                </CardContent>
                <CardActions>
                    <Link to={'/doctors'}>
                        <Button size='small' variant='outlined'>Go back</Button>
                    </Link>
                    <Link to={'/appoitments'}>
                        <Button size='small' variant='outlined'>Make an Appoitment</Button>
                    </Link>
                </CardActions>
            </Card>
        </GridWrapper>
    );
};

export default DoctorDetailsModal;