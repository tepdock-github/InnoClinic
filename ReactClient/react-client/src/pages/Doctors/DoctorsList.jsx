import React from "react";
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import DoctorsTable from "../../components/DoctorsTable/DoctorsTable";

const Doctors = () => {
    const getContent = () => (
        <DoctorsTable/>
    )

    return (
        <GridWrapper>
            <BasicCard
                content={getContent()}
            />
        </GridWrapper>
    )
}

export default Doctors;