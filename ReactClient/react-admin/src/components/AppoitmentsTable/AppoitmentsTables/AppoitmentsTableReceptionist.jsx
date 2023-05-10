import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import { Edit } from '@mui/icons-material';
import { Link } from 'react-router-dom';

const AppoitmentsTableReceptionist = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'id',
                header: 'Id'
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

    const [data, setData] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            var response = await fetch(`http://localhost:7111/gateway/appoitments`, {
                headers: headers
            });

            if (response.status === 200) {
                setData(await response.json());
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
                <Link to={`/appoitment/${row.original.id}`}>
                    <IconButton size='small'>
                        <Edit />
                    </IconButton>
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

export default AppoitmentsTableReceptionist;