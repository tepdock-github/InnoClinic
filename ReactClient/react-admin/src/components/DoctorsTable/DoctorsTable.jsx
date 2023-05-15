import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import { Email } from '@mui/icons-material';
import { Link } from 'react-router-dom';
import { Button } from '@mui/material';

const DoctorsTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'firstName',
                header: 'Имя доктора',
            },
            {
                accessorKey: 'lastName',
                header: 'Фамилия доктора'
            },
            {
                accessorKey: 'specializationName',
                header: 'Специализация'
            },
            {
                accessorKey: 'status',
                header: 'Статус'
            }
        ], []
    );

    const [data, setData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const response = await fetch('http://localhost:7111/gateway/doctors');   
            setData(await response.json());
        }
        fetchData();
    }, []);

    const handleSendMessage = async (row) => {
        console.log(row.original.accountId);
        try {
            const response = await fetch(`http://localhost:7111/gateway/accounts/${row.original.accountId}`);
            if (response.ok) {
                const emailData = await response.json();
                window.open(`mailto:${emailData.email}?subject=Hello ${row.original.firstName}!`);
            } else {
                console.error('Failed to fetch account data:', response.status);
                // Handle the error case accordingly
            }
        } catch (error) {
            console.error('Error fetching account data:', error);
            // Handle the error case accordingly
        }
    }

    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={data}
                enableRowActions
                renderRowActions={({ row }) => (
                    <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                        <IconButton
                            color="primary"
                            onClick={() => handleSendMessage(row)}
                        >
                            <Email />
                        </IconButton>
                        <Link to={`/doctors/${row.original.id}`}>
                            <Button variant='text' color='primary' size='small'>
                                Подробнее
                            </Button>
                        </Link>
                    </Box>
                )}
            />
        </>
    );
};

export default DoctorsTable;