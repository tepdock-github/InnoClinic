import React, { useState } from 'react';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import { Button, Grid, TextField } from '@mui/material';
import GridWrapper from '../common/GridWrapper/GridWrapper';

const validationSchema = Yup.object().shape({
    firstName: Yup.string().required(),
    middleName: Yup.string(),
    lastName: Yup.string().required(),
    dateOfBirth: Yup.date().required('Дата рождения обязательное поле(YYYY-MM-DD)'),
    isLinkedToAccount: Yup.bool().required(),
    accountId: Yup.string().required()
});

const ProfileCreateForm = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    var userid = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        firstName: '',
        middleName: '',
        lastName: '',
        isLinkedToAccount: true,
        dateOfBirth: '',
        accountId: userid
    }

    const handleFormSubmit = async (values, actions) => {
        setIsSubmitting(true);
        try {
            console.log(JSON.stringify(values));
            await fetch(`http://localhost:7111/gateway/patients`, {
                method: 'POST',
                headers: headers,
                body: JSON.stringify(values),
            })

        } catch (error) {
            console.log(error.message);
        }
        setIsSubmitting(false);
    }

    return (
        <GridWrapper>
            <Formik
                initialValues={initialValues}
                validationSchema={validationSchema}
                onSubmit={handleFormSubmit}
            >
                {(formikProps) => (
                    <Form>
                        <Grid container spacing={2} justify="center" alignItems="center" >
                            <Field as={TextField} name='firstName' label='firstName'
                                fullWidth
                                error={formikProps.touched.firstName && !!formikProps.errors.firstName}
                                helperText={formikProps.touched.firstName && formikProps.errors.firstName}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='middleName' label='middleName'
                                fullWidth
                                error={formikProps.touched.middleName && !!formikProps.errors.middleName}
                                helperText={formikProps.touched.middleName && formikProps.errors.middleName}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='lastName' label='lastName'
                                fullWidth
                                error={formikProps.touched.lastName && !!formikProps.errors.lastName}
                                helperText={formikProps.touched.lastName && formikProps.errors.lastName}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='dateOfBirth' label='dateOfBirth'
                                fullWidth
                                error={formikProps.touched.dateOfBirth && !!formikProps.errors.dateOfBirth}
                                helperText={formikProps.touched.dateOfBirth && formikProps.errors.dateOfBirth}
                                margin='normal'
                                variant='outlined'
                            />
                            <Button type='submit' color='primary' disabled={isSubmitting}>
                                Создать
                            </Button>
                            <Link to={'/profile'}>
                                <Button size='small' variant='outlined'>
                                    Отмена
                                </Button>
                            </Link>
                        </Grid>
                    </Form>
                )}
            </Formik>
        </GridWrapper>
    )
};

export default ProfileCreateForm;