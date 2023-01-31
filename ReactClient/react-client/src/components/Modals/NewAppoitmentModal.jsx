import React, { useEffect, useState } from 'react';
import BasicModal from '../common/Modal/BasicModal';
import Box from '@mui/material/Box';
import BasicTimePicker from '../common/DateTimePickers/TimePicker/BasicTimePicker';
import BasicDatePicker from '../common/DateTimePickers/DatePicker/BasicDatePicker';
import Filter from '../../components/common/Filter/Filter';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';

const specializations = [
    {
        id: 0,
        title: 'aboba'
    },
    {
        id: 1,
        title: 'obabo'
    }
]

const services = [
    {
        id: 0,
        title: 'service0'
    },
    {
        id: 1,
        title: 'service1'
    }
]

const doctors = [
    {
        id: 0,
        title: 'doctor'
    },
    {
        id: 1,
        title: 'rotcod'
    }
]

const offices = [
    {
        id: 0,
        title: 'Gukova street 29'
    },
    {
        id: 1,
        title: 'Lebno 345'
    },
    {
        id: 2,
        title: '1tyt'
    }
]

const defaultInputValues = {
    email: '',
    password: ''
}

const NewAppoitmentModal = ({ open, onClose }) => {
    const [values, setValues] = useState(defaultInputValues);

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

    const validationSchema = Yup.object().shape({
        email: Yup.string()
            .required('Email is required')
            .email('Email is invalid'),
        password: Yup.string()
            .required('Password is required')
            .min(6, 'Password must be at least 6 characters')
            .max(12, 'password must be at most 12 characters'),
    });

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm({
        resolver: yupResolver(validationSchema)
    })

    const handleChange = (value) => {
        setValues(value)
    };

    useEffect(() => {
        if (open) setValues(defaultInputValues);
    }, [open])

    const getContent = () => (
        <>
        <Box sx={modalStyles.inputFields}>
            <Filter items={specializations} label={"specialization"}/>
            <Filter items={services} label={'service'}/>
            <Filter items={doctors} label={'doctor'}/>
            <Filter items={offices} label={"office"}/>
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
            validate={handleSubmit()}
        >

        </BasicModal>
    )
}

export default NewAppoitmentModal;