import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import SignInModal from '../Modals/SignInModal';
import { Box, IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';

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

            if (accessToken) {

                const respAppoitments = await fetch(`http://localhost:7111/gateway/appoitments/patient-schedule/${userId}`, {
                    headers: headers
                });

                if (respAppoitments.status === 200) {
                    setData(await respAppoitments.json());
                    setStatusCode(200);
                }
                else setStatusCode(401);

            }
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
                                color='primary'
                                onClick={() => {
                                    console.log(row.original.id);
                                    handleDeleteAppointment(row.original.id)
                                }}
                            >
                                <DeleteIcon />
                            </IconButton>
                        </Box>
                    )}
                />}
            {statusCode === 401 &&
                <SignInModal isOpen={() => handleOpenSignIn()} onClose={() => handleCloseSignIn(false)} />}
        </>
    )
};

export default AppoitmentsTable;