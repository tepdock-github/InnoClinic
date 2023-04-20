import React, { useState } from 'react'
import { DataGrid } from '@mui/x-data-grid';

const DataTable = ({
    rows,
    columns,
    loading,
    sx
}) => {
    const [pageSize, setPageSize] = useState(2);

    return (
        <DataGrid 
            rows={rows}
            columns={columns}
            loading={loading}
            sx={sx}
            pagination
            pageSize={pageSize}
            onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
            rowsPerPageOptions={[5]}
        />
    );
};

export default DataTable;