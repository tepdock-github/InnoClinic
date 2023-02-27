import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import '@fontsource/roboto/400.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import ServicesList from './pages/Services/ServicesList';
import AppoitmentsList from './pages/Appoitments/AppoitmentsList';
import Doctors from './pages/Doctors/DoctorsList';
import ResultsList from './pages/Results/ResultsList';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <BrowserRouter>
    <Routes>
        <Route path='/' element={<App/>}>
            <Route path='/authentication'/>
            <Route path='/services' element={<ServicesList/>}/>
            <Route path='/appoitments' element={<AppoitmentsList/>}/> 
            <Route path='/doctors' element={<Doctors/>}/>
            <Route path='/medical-results' element={<ResultsList/>}/>
        </Route>
    </Routes>
    </BrowserRouter>
);
