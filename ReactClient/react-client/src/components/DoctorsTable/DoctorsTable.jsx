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
            const respDoctors = await fetch('http://localhost:7111/gateway/doctors');
            const resultDoctors = await respDoctors.json();
            setData([...resultDoctors]);
        }
        fetchData();
    }, []);
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
                            onClick={() =>
                                window.open(
                                    `mailto:kolodpoland@gmail.com?subject=Hello ${row.original.firstName}!`,
                                )
                            }
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