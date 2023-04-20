import React from 'react';
import { useParams } from "react-router-dom";
import { useEffect, useState } from 'react'
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import GridWrapper from '../common/GridWrapper/GridWrapper';
import { Link } from 'react-router-dom';

const ServiceDetailModel = () => {
    const { id } = useParams();
    const [service, setService] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const responseService = await fetch(`http://localhost:7111/gateway/services/${id}`);
                if (!responseService.ok) {
                    throw new Error(responseService.statusText);
                }
                setService(await responseService.json());
            } catch (error) {
                console.error(error);
            }
        };
        fetchData();
    }, []);

    return (
        <GridWrapper>
            <Card sx={{ maxWidth: 500 }}>
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        Сервис: {service.serviceName}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        специализация: {service.specializationName} | категория: {service.serviceCategory}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        цена: {service.price} руб
                    </Typography>
                </CardContent>
                <CardActions>
                    <Link to={'/services'}>
                        <Button size='small' variant='outlined'>Go back</Button>
                    </Link>
                    <Link to={'/appoitments'}>
                        <Button size='small' variant='outlined'>Make an Appoitment</Button>
                    </Link>
                </CardActions>
            </Card>
        </GridWrapper>
    );
}

export default ServiceDetailModel;