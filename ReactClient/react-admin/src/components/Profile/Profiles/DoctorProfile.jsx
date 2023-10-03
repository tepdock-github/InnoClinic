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

const DoctorProfile = () => {
    const navigate = useNavigate();
    const [data, setData] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            const response = await fetch(`http://localhost:7111/gateway/doctors/account/${userId}`, { 
                headers: headers
            });

            if (response.status === 200) {
                setData(await response.json());
            }
            else if (response.status === 401) {
                navigate('/401-error');
            }
            else if (response.status === 403) {
                navigate('/403-error')
            }
            else if (response.status === 404) {
                navigate('/profile/doctor/create');
            }
            else navigate('/500-error')
        }
        fetchData();
    }, []);

    return (
        <GridWrapper>
            <Card sx={{ maxWidth: 500, minWidth: 250 }}>
                <CardMedia sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: 75 }}>
                    <BackgroundLetterAvatars
                        firstName={data.firstName}
                        lastName={data.lastName}
                    />
                </CardMedia>
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        ФИО: {data.lastName} {data.firstName} {data.middleName}
                    </Typography>
                    <Typography>
                        Дата рождения: {data.dateOfBirth} Дата старта карьеры: {data.careerStartYear}
                    </Typography>
                    <Typography>
                        Специализация: {data.specializationName}
                    </Typography>
                    <Typography>
                        Статус: {data.status}
                    </Typography>
                </CardContent>
                <CardActions>
                    <Link to={'/profile/edit/doctor'}>
                        <Button size='small' variant='outlined'>Изменить</Button>
                    </Link>
                </CardActions>
            </Card>
        </GridWrapper>
    )
}

export default DoctorProfile;