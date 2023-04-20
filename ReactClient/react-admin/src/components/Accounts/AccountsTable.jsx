import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import { Edit, Delete } from '@mui/icons-material';
import { Link } from 'react-router-dom';
import { Button } from '@mui/material';

const AccountsPatientsTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'firstName',
                header: 'First name',
            },
            {
                accessorKey: 'lastName',
                header: 'Last name'
            },
            {
                accessorKey: 'middleName',
                header: 'Last name'
            },
            {
                accessorKey: 'dateOfBirth',
                header: 'date of birth'
            }
        ]
    )
    const [prP, setPAtient] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            const respPat = await fetch('http://localhost:7111/gateway/patients', {headers});

            const pat = await respPat.json();
            setPAtient(pat);
        }
        fetchData();
    }, [])

    const handleDelete = async (row) => {
        await fetch(`http://localhost:7111/gateway/patients/${row}`, {
            method: 'DELETE',
            headers: headers
        })
    };


    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={prP}
                enableRowActions
                renderRowActions={({ row }) => (
                    <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                        <Link to={`/edit/patients/${row.original.id}`}>
                            <IconButton color='primary' size='small'>
                                <Edit />
                            </IconButton>
                        </Link>
                        <IconButton color='error' size='small' onClick={() => handleDelete(row.original.id)}>
                            <Delete />
                        </IconButton>
                    </Box>
                )}
            />
        </>
    )
}

export default AccountsPatientsTable;