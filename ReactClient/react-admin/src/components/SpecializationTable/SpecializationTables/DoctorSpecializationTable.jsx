import React, { useMemo, useState, useEffect } from 'react';
import MaterialReactTable from 'material-react-table';
import { useNavigate } from 'react-router-dom';

const DoctorSpecializationTable = () => {
    const columns = useMemo(
        () => [
            {
                accessorKey: 'specializationName',
                header: 'Специализация'
            },
        ], []
    );
    const navigate = useNavigate();
    const [data, setData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const resp = await fetch('http://localhost:7111/gateway/specializations');
            if ( resp.status === 500 ) navigate('/500-error');
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
            />
        </>
    );
}

export default DoctorSpecializationTable;