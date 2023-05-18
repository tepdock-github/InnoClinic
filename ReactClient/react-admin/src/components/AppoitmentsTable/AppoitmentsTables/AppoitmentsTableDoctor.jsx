import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton, Button } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import { Link, useNavigate } from 'react-router-dom';

const AppoitmentsTableDoctor = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'id',
                header: 'Id'
            },
            {
                accessorKey: 'patientFirstName',
                header: 'Имя пациента'
            },
            {
                accessorKey: 'patientLastName',
                header: 'Отчество пациента'
            },
            {
                accessorKey: 'serviceName',
                header: 'Сервис'
            },
            {
                accessorKey: 'date',
                header: 'Дата'
            },
            {
                accessorKey: 'time',
                header: 'Время'
            }
        ], []
    );

    const navigate = useNavigate();
    const [data, setData] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            const doctorProfileResponse = await fetch(`http://localhost:7111/gateway/doctors/account/${userId}`, {
                headers: headers
            });
            const doctorProfile = await doctorProfileResponse.json();
            if (doctorProfile.id) {
                var response = await fetch(`http://localhost:7111/gateway/appoitments/doctor-schedule/${doctorProfile.id}`, {
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
                else navigate('/500-error')
            }
        }
        fetchData();
    }, []);

    const handleDeleteAppointment = async (id) => {
        await fetch(`http://localhost:7111/gateway/appoitments/${id}`, {
            method: 'DELETE',
            headers: headers
        })
    };

    const AppoitmentsAction = ({ row, handleDeleteAppointment }) => {
        return (
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
                <Link to={`/appoitment/create-result/${row.original.id}`}>
                    <Button size='small'>
                        Заключение
                    </Button>
                </Link>
            </Box>
        )
    };

    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={data}
                enableRowActions
                renderRowActions={({ row }) => (
                    <AppoitmentsAction
                        row={row}
                        handleDeleteAppointment={handleDeleteAppointment}
                    />
                )}
            />
        </>
    )
};

export default AppoitmentsTableDoctor;