import React, { useState } from 'react';
import {
    Button,
    TextField,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
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
});

const SignUpModal = ({ isOpen, onClose }) => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleSubmit = async (values, { setSubmitting, resetForm }) => {
        setIsSubmitting(true);
        try {
            const response = await fetch('http://localhost:5010/connect/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(values),
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
                    <Formik initialValues={{ email: '', password: '', confirmPassword: '', roles: ["Patient"] }}
                        onSubmit={handleSubmit}
                        validationSchema={validationSchema}>
                        {({ errors, touched, isSubmitting }) => (
                            <Form>
                                <Field as={TextField} name='email' label='Email'
                                    fullWidth
                                    error={touched.email && !!errors.email}
                                    helperText={touched.email && errors.email}
                                    margin='normal'
                                    variant='outlined' />
                                <Field as={TextField} name='password' label='Пароль'
                                    type='password'
                                    fullWidth
                                    error={touched.password && !!errors.password}
                                    helperText={touched.password && errors.password}
                                    margin='normal'
                                    variant='outlined' />
                                <Field as={TextField} name='confirmPassword' label='подтвердите пароль'
                                    type='password'
                                    fullWidth
                                    error={touched.confirmPassword && !!errors.confirmPassword}
                                    helperText={touched.confirmPassword && errors.confirmPassword}
                                    margin='normal'
                                    variant='outlined' />
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