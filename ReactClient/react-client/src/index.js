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
import ResultCard from './components/ResultsTable/ResultCard';
import DoctorDetailsModal from './components/DoctorsTable/DoctorDetailModal';
import ServiceDetailModel from './components/ServicesTable/ServiceDetailModel';
import ProfileCard from './components/Profile/ProfileCard';
import ProfileForm from './components/Modals/ProfileForm';
import ProfileCreateForm from './components/Modals/ProfileCreateForm';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <BrowserRouter>
    <Routes>
        <Route path='/' element={<App/>}>
            <Route path='/services' element={<ServicesList/>}/>
            <Route path='/services/:id' element={<ServiceDetailModel/>}/>
            <Route path='/appoitments' element={<AppoitmentsList/>}/>
            <Route path='/create/appoitments' element={<NewAppoitmentForm/>}/>
            <Route path='/history/appoitments' element={<AppoitmentsHistoryList/>}/> 
            <Route path='/doctors' element={<Doctors/>}/>
            <Route path='/doctors/:id' element={<DoctorDetailsModal/>}/>
            <Route path='/results' element={<ResultsList/>}/>
            <Route path='/appoitment/result/:id' element={<ResultCard/>}/>
            <Route path='/profile' element={<ProfileCard/>}/>
            <Route path='/profile/edit' element={<ProfileForm/>}/>
            <Route path='/profile/create' element={<ProfileCreateForm/>}/>
        </Route>
    </Routes>
    </BrowserRouter>
);
