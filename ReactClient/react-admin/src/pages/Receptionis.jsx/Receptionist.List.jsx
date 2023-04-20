import React from "react";
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import ReceptionistTable from "../../components/ReceptionistTable/ReceptionistTable";

const ReceptionistList = () => {
    const getContent = () => (
        <ReceptionistTable/>
    )

    return (
        <GridWrapper>
            <BasicCard
                content={getContent()}
            />
        </GridWrapper>
    )
}

export default ReceptionistList;