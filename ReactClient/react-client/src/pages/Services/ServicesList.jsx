import React from 'react';
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import ServiceTable from '../../components/ServicesTable/ServicesTable';

const ServicesList = () => {
    const getHeader = () => {
       
    };

    const getContent = () => (
        <ServiceTable/>
    );

    return (
        <GridWrapper>
            <BasicCard
                header={getHeader()}
                content={getContent()}
            />
        </GridWrapper>
    )
}

export default ServicesList;