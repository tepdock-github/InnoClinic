import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import { useNavigate } from 'react-router-dom';

const DoctorScheduleTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'id',
                header: 'Id'
            },
            {
                accessorKey: 'date',
                header: 'Дата'
            },
            {
                accessorKey: 'time',
                header: 'Время'
            },
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
            var response = await fetch(`http://localhost:7111/gateway/schedules/doctor/${userId}`, {
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
        fetchData();
    }, []);

    const handleDeleteSchedule = async (id) => {
        await fetch(`http://localhost:7111/gateway/schedules/${id}`, {
            method: 'DELETE',
            headers: headers
        })
    };

    const ScheduleAction = ({ row, handleDeleteSchedule }) => {
        return (
            <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                <IconButton
                    color='error'
                    onClick={() => {
                        console.log(row.original.id);
                        handleDeleteSchedule(row.original.id)
                    }}
                >
                    <DeleteIcon />
                </IconButton>
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
                    <ScheduleAction
                        row={row}
                        handleDeleteAppointment={handleDeleteSchedule}
                    />
                )}
            />
        </>
    )
}

export default DoctorScheduleTable;