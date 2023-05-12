import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import { Button } from '@mui/material';

const DoctorServiceTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'serviceName',
                header: 'Service'
            },
            {
                accessorKey: 'price',
                header: 'Price'
            },
        ], []
    );
    const [services, setServices] = useState([]);
    const navigate = useNavigate();

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const getData = async () => {
            const responseService = await fetch('http://localhost:7111/gateway/services');
            if (responseService.status === 500) {
                navigate('/500-error')
            } else {
                setServices(await responseService.json());
            }
        }
        getData();
    }, []);

    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={services}
                enableRowActions
                renderRowActions={({ row }) => (
                    <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                        <Link to={`/services/${row.original.id}`}>
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

export default DoctorServiceTable;