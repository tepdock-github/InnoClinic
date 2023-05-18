import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import { Email } from '@mui/icons-material';
import { Link } from 'react-router-dom';
import { Button } from '@mui/material';

const ReceptionistTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'firstName',
                header: 'Имя',
            },
            {
                accessorKey: 'middleName',
                header: 'фамилия',
            },
            {
                accessorKey: 'lastName',
                header: 'Отчество'
            }           
        ]
    )

    const [recept, setRecept] = useState([]);
    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const getData = async () => {
            const responseService = await fetch('http://localhost:7111/gateway/receptionists', {headers});
            setRecept(await responseService.json());
        }
        getData();
    }, []);

    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={recept}
                enableRowActions
                renderRowActions={({row}) => (
                    <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                        <Link to={`/receptionist/${row.original.id}`}>
                            <Button variant='text' color='primary' size='small'>
                                Подробнее
                            </Button>
                        </Link>
                    </Box>
                )}
            />
        </>
    );
}

export default ReceptionistTable;