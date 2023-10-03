import React, { useState } from 'react';
import {
    Button,
    TextField,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
    Select,
    MenuItem,
} from '@mui/material';
import { Formik, Form, Field } from 'formik';
import * as yup from 'yup';

const validationSchema = yup.object().shape({
    email: yup.string()
        .email()
        .required('Email is required'),
    password: yup.string()
        .required('Password is required')
        .min(6, 'Password must be at least 6 characters')
        .max(12, 'password must be at most 12 characters'),
    confirmPassword: yup.string()
        .required('Confirm password is required')
        .min(6, 'Password must be at least 6 characters')
        .max(12, 'password must be at most 12 characters'),
    roles: yup.string().required()
});

const SignUpModal = ({ isOpen, onClose }) => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleSubmit = async (values, { setSubmitting, resetForm }) => {
        setIsSubmitting(true);
        console.log(values);
        const account = {
            email: values.email,
            password: values.password,
            confirmPassword: values.confirmPassword,
            roles: [
                values.roles
            ]
        }
        console.log(account);
        try {
            const response = await fetch('http://localhost:5010/connect/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(account),
            });
            if (response.ok) {
                resetForm();
                onClose();
                alert('Регистрация успешна! Пожалуйста авторизируйтесь');
            }
            else {
                const errorResponse = await response.json();
                for (const key in errorResponse) {
                    if (errorResponse.hasOwnProperty(key)) {
                        const errorMessage = errorResponse[key][0];
                        alert(`Error for ${key}: ${errorMessage}`);
                    }
                }
            }
        } catch (error) {
            console.error(error);
            alert('Что-то пошло не так. Попробуйте через некоторое время');
        }
        setIsSubmitting(false);
    };

    return (
        <>
            <Dialog open={isOpen} onClose={onClose}>
                <DialogTitle>Регистрация</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Введите ваш email и пароль для создания аккаунта
                    </DialogContentText>
                    <Formik initialValues={{ email: '', password: '', confirmPassword: '', roles: '' }}
                        onSubmit={handleSubmit}
                        validationSchema={validationSchema}>
                        {(formikProps, isSubmitting) => (
                            <Form>
                                <Field as={TextField} name='email' label='Email'
                                    fullWidth
                                    error={formikProps.touched.email && !!formikProps.errors.email}
                                    helperText={formikProps.touched.email && formikProps.errors.email}
                                    margin='normal'
                                    variant='outlined' />
                                <Field as={TextField} name='password' label='Пароль'
                                    type='password'
                                    fullWidth
                                    error={formikProps.touched.password && !!formikProps.errors.password}
                                    helperText={formikProps.touched.password && formikProps.errors.password}
                                    margin='normal'
                                    variant='outlined' />
                                <Field as={TextField} name='confirmPassword' label='подтвердите пароль'
                                    type='password'
                                    fullWidth
                                    error={formikProps.touched.confirmPassword && !!formikProps.errors.confirmPassword}
                                    helperText={formikProps.touched.confirmPassword && formikProps.errors.confirmPassword}
                                    margin='normal'
                                    variant='outlined' />
                                <Select
                                    fullWidth
                                    id='roles'
                                    name='roles'
                                    label='Роль'
                                    value={[formikProps.values.roles]}
                                    onChange={(e) => formikProps.handleChange(e)}
                                    onBlur={formikProps.handleBlur}
                                    error={formikProps.touched.roles && !!formikProps.errors.roles}
                                >
                                    <MenuItem value='Choose role'>
                                        <em>Choose role</em>
                                    </MenuItem>
                                    <MenuItem value='Receptionist'>
                                        <em>администратор</em>
                                    </MenuItem>
                                    <MenuItem value='Doctor'>
                                        <em>врач</em>
                                    </MenuItem>
                                </Select>
                                <DialogActions>
                                    <Button onClick={onClose} disabled={isSubmitting}>
                                        Отмена
                                    </Button>
                                    <Button type='submit' color='primary' disabled={isSubmitting}>
                                        Регистрация
                                    </Button>
                                </DialogActions>
                            </Form>
                        )}
                    </Formik>
                </DialogContent>
            </Dialog>
        </>
    )
}

export default SignUpModal;