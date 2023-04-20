import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import SignInModal from '../Modals/SignInModal';
import { Box, IconButton, Button } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import { Edit } from '@mui/icons-material';
import { Link } from 'react-router-dom';

const AppoitmentsTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'id',
                header: 'N'
            },
            {
                accessorKey: 'doctorFirstName',
                header: 'Имя доктора'
            },
            {
                accessorKey: 'doctorLastName',
                header: 'Отчество доктора'
            },
            {
                accessorKey: 'patientFirstName',
                header: 'Имя pateint'
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
    var role = localStorage.getItem('role');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const getAppoitments = async () => {
            var respAppoitments;

            if(role === 'Doctor'){
                respAppoitments = await fetch(`http://localhost:7111/gateway/appoitments/doctor-schedule/${userId}`, {
                    headers: headers
                });
                console.log(await respAppoitments.json());
            }

            if(role === 'Receptionist'){
                    respAppoitments = await fetch(`http://localhost:7111/gateway/appoitments`, {
                    headers: headers
                });
            }
            if (respAppoitments.status === 200) {
                setData(await respAppoitments.json());
                setStatusCode(200);
            }
            else setStatusCode(401);
        }
        getAppoitments();
    }, []);

    const handleDeleteAppointment = async (id) => {
        await fetch(`http://localhost:7111/gateway/appoitments/${id}`, {
            method: 'DELETE',
            headers: headers
        })
        console.log('Deleting appointment:');
    };

    return (
        <>
            {statusCode === 200 &&
                <MaterialReactTable
                    columns={columns}
                    data={data}
                    enableRowActions
                    renderRowActions={({ row }) => (
                        <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                            <IconButton
                                color='error'
                                onClick={() => {
                                    console.log(row.original.id);
                                    handleDeleteAppointment(row.original.id)
                                }}
                            >
                                <DeleteIcon />
                            </IconButton>
                            <Link to={`/appoitment/${row.original.id}`}>
                            <IconButton size='small'>
                                <Edit/>
                            </IconButton>
                        </Link>
                        </Box>
                    )}
                />}
            {statusCode === 401 &&
                <SignInModal isOpen={() => handleOpenSignIn()} onClose={() => handleCloseSignIn(false)} />}
        </>
    )
};

export default AppoitmentsTable;