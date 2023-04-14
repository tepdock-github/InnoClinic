import React, { useEffect, useState } from 'react';
import BasicModal from '../common/Modal/BasicModal';
import Box from '@mui/material/Box';
import BasicTimePicker from '../common/DateTimePickers/TimePicker/BasicTimePicker';
import BasicDatePicker from '../common/DateTimePickers/DatePicker/BasicDatePicker';
import Filter from '../../components/common/Filter/Filter';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';

const NewAppoitmentModal = ({ open, onClose }) => {

    const [specializations, setSpecializations] = useState([]);
    const [services, setServices] = useState([]);
    const [doctors, setDoctors] = useState([]);
    const [offices, setOffices] = useState([]);

    const modalStyles = {
        inputFields: {
          display: 'flex',
          flexDirection: 'column',
          marginTop: '20px',
          marginBottom: '15px',
          '.MuiFormControl-root': {
            marginBottom: '20px',
          },
        },
      };

    useEffect(() => {
        const fetchAllData = async () => {
            try {
                const [
                    specializationsResponse,
                    servicesResponse,
                    doctorsResponse,
                    officesResponse
                ] = await Promise.all([
                    fetch('http://localhost:7111/gateway/specializations'),
                    fetch('http://localhost:7111/gateway/services'),
                    fetch('http://localhost:7111/gateway/doctors'),
                    fetch('http://localhost:7111/gateway/offices')
                ]);

                const specializationsData = await specializationsResponse.json();
                const servicesData = await servicesResponse.json();
                const doctorsData = await doctorsResponse.json();
                const officesData = await officesResponse.json();

                setSpecializations(specializationsData);
                
                setServices(servicesData);
                setDoctors(doctorsData);
                setOffices(officesData);

            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        if (open) {
            fetchAllData();
        }
    }, [open]);

    const getContent = () => (
        <>
        <Box sx={modalStyles.inputFields}>
            <Filter items={doctors} label={'doctor'}/>
            <BasicDatePicker/>
            <BasicTimePicker/>
        </Box>
        <Box>
            
        </Box>
        </>
    )

    return (
        <BasicModal
            open={open}
            onClose={onClose}
            title="Book an Appoitment"
            subTitle="Choose from below and submit"
            content={getContent()}
        >

        </BasicModal>
    )
}

export default NewAppoitmentModal;