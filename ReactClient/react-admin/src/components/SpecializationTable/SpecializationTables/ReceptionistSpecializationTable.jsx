import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import { Edit } from '@mui/icons-material';
import { Link } from 'react-router-dom';

const ReceptionistSpecializationTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'specializationName',
                header: 'Специализация'
            },
        ], []
    );
    const [data, setData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const resp = await fetch('http://localhost:7111/gateway/specializations');
            const result = await resp.json();
            setData(result);
        }
        fetchData();
    }, []);

    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={data}
                enableRowActions
                renderRowActions={({row}) => (
                    <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                        <Link to={`/edit/specializations/${row.original.id}`}>
                            <IconButton color='primary' size='small'>
                                <Edit/>
                            </IconButton>
                        </Link>
                    </Box>
                )}
            />
        </>
    )
}

export default ReceptionistSpecializationTable;