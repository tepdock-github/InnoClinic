import React from "react";
import GridWrapper from '../../components/common/GridWrapper/GridWrapper';
import BasicCard from '../../components/common/Card/BasicCard';
import AccountsPatientsTable from "../../components/Accounts/AccountsTable";

const Accounts = () => {
    const getContent = () => (
        <AccountsPatientsTable/>
    )

    return (
        <GridWrapper>
            <BasicCard
                content={getContent()}
            />
        </GridWrapper>
    )
}

export default Accounts;