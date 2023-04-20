import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import { Edit } from '@mui/icons-material';
import { Link } from 'react-router-dom';
import { Button } from '@mui/material';

const OfficesTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'address',
                header: 'Адрес'
            },
            {
                accessorKey: 'phoneNumber',
                header: 'Телефонный номер'
            }
        ], []
    );
    const [offices, setOffices] = useState([]);

    useEffect(() => {
        const getData= async () => {
            const response = await fetch('http://localhost:7111/gateway/offices');
            setOffices(await response.json());
        }
        getData();
    }, [])

    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={offices}
                enableRowActions
                renderRowActions={({row}) => (
                    <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                        <Link to={`/edit/offices/${row.original.id}`}>
                            <IconButton color='primary' size='small'>
                                <Edit/>
                            </IconButton>
                        </Link>
                    </Box>
                )}
            />
        </>
    );

};

export default OfficesTable;