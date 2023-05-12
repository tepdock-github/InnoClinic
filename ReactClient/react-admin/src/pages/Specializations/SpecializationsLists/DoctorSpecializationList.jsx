import React from 'react';
import GridWrapper from '../../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../../components/common/Card/BasicCard';
import SpecializationTable from '../../../components/SpecializationTable/SpecializationTable';

const DoctorSpecializationList = () => {
    const getContent = () => (
        <SpecializationTable />
    );

    return (
        <GridWrapper>
            <BasicCard
                content={getContent()}
            />
        </GridWrapper>
    )
}

export default DoctorSpecializationList;