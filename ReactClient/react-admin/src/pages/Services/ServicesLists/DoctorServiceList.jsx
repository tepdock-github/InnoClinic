import React from 'react';
import GridWrapper from '../../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../../components/common/Card/BasicCard';
import ServiceTable from '../../../components/ServicesTable/ServicesTable';

const DoctorServiceList = () => {
    const getContent = () => (
        <ServiceTable />
    );

    return (
        <GridWrapper>
            <BasicCard
                content={getContent()}
            />
        </GridWrapper>
    )
}

export default DoctorServiceList;