import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import { Link } from 'react-router-dom';
import { Button } from '@mui/material';

const ServiceTable = () => {
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

    useEffect(() => {
        const getData = async () => {
            const responseService = await fetch('http://localhost:7111/gateway/services');
            setServices(await responseService.json());
        }
        getData();
    }, []);

    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={services}
                enableRowActions
                renderRowActions={({row}) => (
                    <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                        <Link to={`/services/${row.original.id}`}>
                            <Button variant='text' color='primary' size='small'>
                                View Details
                            </Button>
                        </Link>
                    </Box>
                )}
            />
        </>
    );
};

export default ServiceTable;