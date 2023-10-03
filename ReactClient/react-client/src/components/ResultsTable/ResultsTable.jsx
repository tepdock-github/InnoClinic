import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import SignInModal from '../Modals/SignInModal';
import { Box, Button } from '@mui/material';

const ResultsTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'id',
                header: 'N'
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
    const [data, setData] = useState([]);
    const [statusCode, setStatusCode] = useState([]);
    const [openSignIn, setOpenSignIn] = useState(false);
    const handleOpenSignIn = () => {
        setOpenSignIn(true);
    }
    const handleCloseSignIn = () => {
        setOpenSignIn(false);
    }

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            const patientProfileResponse = await fetch(`http://localhost:7111/gateway/patients/account/${userId}`, {
                headers: headers
            });
            const patientProfile = await patientProfileResponse.json();
            if (patientProfile.id) {
                const respResults = await fetch(`http://localhost:7111/gateway/results/patient/${patientProfile.id}`, {
                    method: 'GET',
                    headers: headers
                })

                if (respResults.status === 200) {
                    setData(await respResults.json());
                    setStatusCode(200);
                }
                else setStatusCode(401);
            }
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
            {statusCode === 200 &&
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
                        </Box>
                    )}
                />}
            {statusCode === 401 &&
                <SignInModal isOpen={() => handleOpenSignIn(true)} onClose={() => handleCloseSignIn(false)} />}
        </>
    )
};

export default ResultsTable;