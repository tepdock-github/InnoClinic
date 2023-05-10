import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { Link, useNavigate } from 'react-router-dom';
import { Box, Button } from '@mui/material';

const ReceptionistResultTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'id',
                header: 'Id'
            },
            {
                accessorKey: 'complaints',
                header: 'Жалобы'
            },
            {
                accessorKey: 'conclusion',
                header: 'Заключение'
            },
            {
                accessorKey: 'recomendations',
                header: 'Рекомендации'
            },
            {
                accessorKey: 'diagnosis',
                header: 'Диагнозис'
            }
        ], []
    );

    const navigate = useNavigate();
    const [data, setData] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            var response = await fetch('http://localhost:7111/gateway/results', {
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

    const handleDownloadResult = async (filename) => {
        var file = `Appoitment${filename}.pdf`;
        var response = await fetch(`http://localhost:7111/gateway/storage/${file}`, {
            method: 'GET',
            headers: headers
        })
        if (response.ok) {
            const blob = await response.blob();
            // Create a temporary anchor element to download the file
            const anchor = document.createElement('a');
            anchor.href = URL.createObjectURL(blob);
            anchor.download = file;
            document.body.appendChild(anchor);
            anchor.click();
            document.body.removeChild(anchor);
        } else {
            console.error('Failed to download file:', response.status, response.statusText);
        }
        console.log('Get appointment:');
    };

    return (
        <>
            <MaterialReactTable
                columns={columns}
                data={data}
                enableRowActions
                renderRowActions={({ row }) => (
                    <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '8px' }}>
                        <Button
                            color='primary'
                            onClick={() => {
                                console.log(row.original.appoitmentId);
                                handleDownloadResult(row.original.appoitmentId)
                            }}
                        >
                            Скачать
                        </Button>
                        <Link to={`/appoitment/result/${row.original.appoitmentId}`}>
                            <Button variant='text' color='primary' size='small'>
                                Детали
                            </Button>
                        </Link>
                    </Box>
                )}
            />
        </>
    );
}

export default ReceptionistResultTable;