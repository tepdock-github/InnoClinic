import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import SignInModal from '../Modals/SignInModal';
import { Link } from 'react-router-dom';
import { Box, Button } from '@mui/material';

const AppoitmentsHistoryTables = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'id',
                header: 'N'
            },
            {
                accessorKey: 'patientFirstName',
                header: 'Имя patient'
            },
            {
                accessorKey: 'patientLastName',
                header: 'Отчество patient'
            },
            {
                accessorKey: 'serviceName',
                header: 'сервис'
            },
            {
                accessorKey: 'date',
                header: 'Дата'
            },
            {
                accessorKey: 'time',
                header: 'время'
            }
        ], []
    );

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
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const getAppoitments = async () => {

            const doctorProfileResponse = await fetch(`http://localhost:7111/gateway/doctors/account/${userId}`, {
                headers: headers
            });
            const doctorProfile = await doctorProfileResponse.json();
            if (doctorProfile.id){
                const respAppoitments = await fetch(`http://localhost:7111/gateway/appoitments/doctor-history/${doctorProfile.id}`, {
                    headers: headers
                });

                if (respAppoitments.status === 200) {
                    setData(await respAppoitments.json());
                    setStatusCode(200);
                }
                else {
                    setStatusCode(401);
                    handleOpenSignIn();
                }
            }
        }
        getAppoitments();
    }, []);

    useEffect(() => {
        if (statusCode !== 401) {
            handleCloseSignIn();
        }
    }, [statusCode]);
    
    return (
        <>
            {statusCode === 200 &&
                <MaterialReactTable
                    columns={columns}
                    data={data}
                    enableRowActions
                    renderRowActions={({row}) => (
                        <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                            <Link to={`/appoitment/result/${row.original.id}`}>
                            <Button variant='text' color='primary' size='small'>
                                Посмотреть заключение
                            </Button>
                        </Link>
                        </Box>
                    )}
                />}
            {statusCode === 401 &&
                <SignInModal isOpen={openSignIn} onClose={() => handleCloseSignIn(false)} />}
        </>
    )
};

export default AppoitmentsHistoryTables;