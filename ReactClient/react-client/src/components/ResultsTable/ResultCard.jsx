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
import SignInModal from '../Modals/SignInModal';

const ResultCard = () => {
    const { id } = useParams();
    const [result, setResult] = useState();
    const [statusCode, setStatusCode] = useState([]);
    const [openSignIn, setOpenSignIn] = useState(false);

    const handleOpenSignIn = () => {
        setOpenSignIn(true);
    }
    const handleCloseSignIn = () => {
        setOpenSignIn(false);
    }

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchResult = async () => {
            const response = await fetch(`http://localhost:7111/gateway/results/appoitment/${id}`, {
                headers: headers
            });

            if (response.status === 200) {
                setResult(await response.json());
                setStatusCode(200);
            }
            else {
                setStatusCode(401);
                handleOpenSignIn();
            }
        };
        fetchResult();
    }, [])

    useEffect(() => {
        if (statusCode !== 401) {
            handleCloseSignIn();
        }
    }, [statusCode]);

    return (
        <>
            {statusCode === 200 &&
                <GridWrapper>
                    <Card sx={{ maxWidth: 750, minWidth: 500 }}>
                        <CardContent>
                            <Typography gutterBottom variant="h5" component="div">
                                результат N{result.id}: Запись {result.appoitmentId}
                            </Typography>
                            <Typography variant="body2" color="text.secondary">
                                Жалобы: {result.complaints}
                            </Typography>
                            <Typography variant="body2" color="text.secondary">
                                Заключение: {result.conclusion}
                            </Typography>
                            <Typography variant="body2" color="text.secondary">
                                Рекомендации: {result.recomendations}
                            </Typography>
                            <Typography variant="body2" color="text.secondary">
                                Диагнозис: {result.diagnosis}
                            </Typography>
                        </CardContent>
                        <CardActions>
                            <Link to={'/appoitments'}>
                                <Button size='small' variant='outlined'>Go back</Button>
                            </Link>
                        </CardActions>
                    </Card>
                </GridWrapper>}
                {statusCode === 401 &&
                <SignInModal isOpen={openSignIn} onClose={() => handleCloseSignIn(false)} />}
        </>
    )
}

export default ResultCard;