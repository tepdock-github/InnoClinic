import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Box, IconButton } from '@mui/material';
import { Link } from 'react-router-dom';
import { Button } from '@mui/material';

const AppoitmentsTable = ({onError}) => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'id',
                header: 'N'
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
                accessorKey: 'serviceName',
                header: 'сервис'
            },
            {
                accessorKey: 'date',
                header: 'Дата'
            },
            {
                accessorKey: 'time',
                header: 'время'
            }
        ], []
    );

    const [data, setData] = useState([]);

    useEffect(() => {
        const getAppoitments = async () => {
            var accessToken = localStorage.getItem('accessToken');
            var userId = localStorage.getItem('userId');

            if(accessToken){
                const header = {
                    Authorization: `Bearer ${accessToken}`
                }

                const respAppoitments = await fetch(`http://localhost:7111/gateway/appoitments/${userId}`, {
                headers: header
            });
            setData(await respAppoitments.json());
            }
        }
        getAppoitments();
    }, []);

    return (
        <>
        <MaterialReactTable
            columns={columns}
            data={data}
            >
            
        </MaterialReactTable>
        </>
    )
};

export default AppoitmentsTable;