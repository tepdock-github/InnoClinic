import React from 'react';
import SignInModal from '../Modals/SignInModal';
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

const ProfileCard = () => {
    const navigate = useNavigate();
    const [data, setData] = useState([]);
    const [statusCode, setStatusCode] = useState([]);
    const [openSignIn, setOpenSignIn] = useState(false);

    const handleOpenSignIn = () => {
        setOpenSignIn(true);
    }
    const handleCloseSignIn = () => {
        setOpenSignIn(false);
    }

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    var role = localStorage.getItem('role');
    console.log(role);
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            let respProfile;
            if (role === 'Doctor') {
                respProfile = await fetch(`http://localhost:7111/gateway/doctors/account/${userId}`, {
                    method: 'GET',
                    headers: headers
                })
            }
            if (role === 'Receptionist') {

                respProfile = await fetch(`http://localhost:7111/gateway/receptionists/account/${userId}`, {
                    method: 'GET',
                    headers: headers
                })
            }

            if (respProfile.status === 200) {
                setData(await respProfile.json());
                setStatusCode(200);
            }
            else if (respProfile.status === 401) {
                setStatusCode(401);
                handleOpenSignIn();
            }
            else if (respProfile.status === 404) {
                if (role === 'Doctor')
                    navigate('/profile/doctor/create');
                if (role === "Receptionist")
                    navigate('/profile/receptionist/create')
            }
        }
        fetchData();
    }, []);

    return (
        <>
            {statusCode === 200 &&
                <GridWrapper>
                    <Card sx={{ maxWidth: 500, minWidth: 250 }}>
                        <CardMedia>
                            <BackgroundLetterAvatars
                                firstName={data.firstName}
                                lastName={data.lastName}
                            />
                        </CardMedia>
                        {role === 'Doctor' &&
                            <>
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
                            </>}
                        {role === 'Receptionist' &&
                        <>
                        <CardContent>
                            <Typography gutterBottom variant="h5" component="div">
                                ФИО: {data.lastName} {data.firstName} {data.middleName}
                            </Typography>
                        </CardContent>
                        <CardActions>
                            <Link to={'/profile/edit/receptionist'}>
                                <Button size='small' variant='outlined'>Изменить</Button>
                            </Link>
                        </CardActions>
                        </>}    
                    </Card>
                </GridWrapper>}
            {statusCode === 401 &&
                <SignInModal isOpen={openSignIn} onClose={() => handleCloseSignIn(false)} />}
        </>
    )
}

export default ProfileCard;