import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import '@fontsource/roboto/400.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import ServicesList from './pages/Services/ServicesList';
import AppoitmentsList from './pages/Appoitments/AppoitmentsList';
import AppoitmentsHistoryList from './pages/Appoitments/AppoitmentsHistory';
import NewAppoitmentForm from './components/Modals/NewAppoitmentModal';
import Doctors from './pages/Doctors/DoctorsList';
import ResultsList from './pages/Results/ResultsList';
import DoctorDetailsModal from './components/DoctorsTable/DoctorDetailModal';
import ServiceDetailModel from './components/ServicesTable/ServiceDetailModel';
import ProfileCard from './components/Profile/ProfileCard';
import ProfileForm from './components/Modals/ProfileForm';
import DoctorProfileCreateForm from './components/Modals/ProfileCreateForm';
import CreateReceptionistProfile from './components/Modals/CreateReceptionistProfile';
import OfficesList from './pages/Offices/OfficesList';
import NewOfficeModal from './components/Modals/NewOfficeModal';
import NewServiceModal from './components/Modals/NewServiceModal';
import EditServiceModal from './components/Modals/EditServiceModal';
import SpecializationList from './pages/Specializations/SpecializationsList';
import NewSpecialization from './components/Modals/NewSpecialization';
import EditSpecializations from './components/Modals/EditSpecializations';
import EditReceptionist from './components/Modals/EditReceptionist';
import Accounts from './pages/Accounts/AccountsList';
import ProfilePatientForm from './components/Modals/ProfileFormPatient';
import ReceptionistList from './pages/Receptionis.jsx/Receptionist.List';
import ReceptionistDetailsModal from './components/Profile/ReceptionistProfile';
import EditAppoitmentForm from './components/Modals/EditAppoitmentForm';
import NewResultForm from './components/Modals/NewResultForm';
import ResultCard from './components/ResultsTable/ResultCard';
import NewScheduleForm from './components/Modals/NewScheduleForm';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <BrowserRouter>
    <Routes>
        <Route path='/' element={<App/>}>
            <Route path='/accounts' element={<Accounts/>}/>
            <Route path='/offices' element={<OfficesList/>}/>
            <Route path='/schedules' element={<NewScheduleForm/>}/>
            <Route path='/create/office' element={<NewOfficeModal/>}/>
            <Route path='/services' element={<ServicesList/>}/>
            <Route path='/create/service' element={<NewServiceModal/>}/>
            <Route path='/edit/services/:id' element={<EditServiceModal/>}/> 
            <Route path='/services/:id' element={<ServiceDetailModel/>}/>
            <Route path='/create/specializations' element={<NewSpecialization/>}/>
            <Route path='/edit/specializations/:id' element={<EditSpecializations/>}/>
            <Route path='/specializations' element={<SpecializationList/>}/>
            <Route path='/appoitments' element={<AppoitmentsList/>}/>
            <Route path='/appoitment/:id' element={<EditAppoitmentForm/>}/> 
            <Route path='/appoitment/create-result/:id' element={<NewResultForm/>}/>
            <Route path='/create/appoitments' element={<NewAppoitmentForm/>}/>
            <Route path='/history/appoitments' element={<AppoitmentsHistoryList/>}/> 
            <Route path='/doctors' element={<Doctors/>}/>
            <Route path='/doctors/:id' element={<DoctorDetailsModal/>}/>
            <Route path='/results' element={<ResultsList/>}/>
            <Route path='/appoitment/result/:id' element={<ResultCard/>}/>
            <Route path='/profile' element={<ProfileCard/>}/>
            <Route path='/profile/edit/doctor' element={<ProfileForm/>}/>
            <Route path='/edit/patients/:id' element={<ProfilePatientForm/>}/>
            <Route path='/profile/edit/receptionist' element={<EditReceptionist/>}/>
            <Route path='/profile/doctor/create' element={<DoctorProfileCreateForm/>}/>
            <Route path='/profile/receptionist/create' element={<CreateReceptionistProfile/>}/>
            <Route path='/receptionist' element={<ReceptionistList/>}/>
            <Route path='/receptionist/:id' element={<ReceptionistDetailsModal/>}/>
        </Route>
    </Routes>
    </BrowserRouter>
);
