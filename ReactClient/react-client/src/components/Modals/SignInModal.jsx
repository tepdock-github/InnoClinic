import React, { useEffect, useState } from 'react';
import BasicModal from '../common/Modal/BasicModal';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';


const defaultInputValues = {
    email: '',
    password: ''
}

const SignInModal = ({ open, onClose }) => {
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
        <Box sx={modalStyles.inputFields}>
            <TextField
                placeholder='email'
                name='email'
                label='email'
                required
                {...register('email')}
                value={values.email}
                onChange={(event)=> handleChange({...values, email: event.target.value})}
                error={errors.email ? true : false}
                helperText={errors.email?.message}
            />
            <TextField
                placeholder='password'
                name='password'
                type='password'
                label='password'
                required
                {...register('password')}
                value={values.password}
                onChange={(event)=> handleChange({...values, password: event.target.value})}
            />
        </Box>
    )

    return (
        <BasicModal
            open={open}
            onClose={onClose}
            title="Sign In"
            subTitle="Sign In to contineu to use our services"
            content={getContent()}
            validate={handleSubmit()}
        >

        </BasicModal>
    )
}

export default SignInModal;