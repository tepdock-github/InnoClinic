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


const validationSchemaSignIn = yup.object().shape({
    username: yup.string()
        .email()
        .required('Email is required'),
    password: yup.string()
        .required('Password is required')
        .min(6, 'Password must be at least 6 characters')
        .max(12, 'password must be at most 12 characters'),
});

const SignInModal = ({ isOpen, onClose }) => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleSubmit = async (values, { setSubmitting, resetForm }) => {
        console.log("a");
        setIsSubmitting(true);
        try {
            console.log(JSON.stringify(values))
            const response = await fetch('http://localhost:5010/connect/token', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: new URLSearchParams({ 
                    username: values.username,
                    password: values.password,
                    grant_type: 'password',
                    client_id: 'authMicroservice',
                    client_secret: 'innoClinicSecret',
                    scope: ['gatewayAPI.scope']
                }),
            });
            if (response.ok) {
                console.log(JSON.stringify(values));
                response.json()
                    .then(data => {
                        console.log(data);
                        localStorage.setItem("accessToken", data.access_token);
                        const parts = data.access_token.split('.');
                        const decodedPayload = JSON.parse(atob(parts[1]));
                        console.log(decodedPayload.id);
                        localStorage.setItem("userId", decodedPayload.id);
                    })
                resetForm();
                onClose();
                alert('авторизация успешна!');
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
                <DialogTitle>Авторизация</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Введите ваш email и пароль для авторизации
                    </DialogContentText>
                    <Formik initialValues={{
                        username: '',
                        password: '',
                        grant_type: 'password',
                        client_id: 'authMicroservice',
                        client_secret: 'innoClinicSecret',
                        scope: ['gatewayAPI.scope']
                    }}
                        onSubmit={handleSubmit}
                        validationSchema={validationSchemaSignIn}>
                        {({ errors, touched, isSubmitting }) => (
                            <Form>
                                <Field as={TextField} name='username' label='Email'
                                    fullWidth
                                    error={touched.username && !!errors.username}
                                    helperText={touched.username && errors.username}
                                    margin='normal'
                                    variant='outlined' />
                                <Field as={TextField} name='password' label='Пароль'
                                    type='password'
                                    fullWidth
                                    error={touched.password && !!errors.password}
                                    helperText={touched.password && errors.password}
                                    margin='normal'
                                    variant='outlined' />
                                <Field
                                    as={TextField}
                                    type='hidden'
                                    name='grant_type'
                                    value='password'
                                />
                                <Field
                                    as={TextField}
                                    type='hidden'
                                    name='client_id'
                                    value='authMicroservice'
                                />
                                <Field
                                    as={TextField}
                                    type='hidden'
                                    name='client_secret'
                                    value='innoClinicSecret'
                                />
                                <Field
                                    as={TextField}
                                    type='hidden'
                                    name='scope'
                                />
                                <DialogActions>
                                    <Button onClick={onClose} disabled={isSubmitting}>
                                        Отмена
                                    </Button>
                                    <Button type='submit' color='primary' disabled={isSubmitting}>
                                        Авторизация
                                    </Button>
                                </DialogActions>
                            </Form>
                        )}
                    </Formik>
                </DialogContent>
            </Dialog>
        </>
    );
}

export default SignInModal;