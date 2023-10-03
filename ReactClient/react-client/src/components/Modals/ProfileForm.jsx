import React, { useEffect, useState } from 'react';
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

const ProfileForm = () => {
    const [profile, setProfile] = useState();
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            const respProfile = await fetch(`http://localhost:7111/gateway/patients/account/${userId}`, {headers});
            const profileData = await respProfile.json();

            initialValues.firstName = profileData.firstName;
            initialValues.middleName = profileData.middleName;
            initialValues.lastName = profileData.lastName;
            initialValues.accountId = profileData.accountId;
            initialValues.isLinkedToAccount = profileData.isLinkedToAccount;
            initialValues.dateOfBirth = profileData.dateOfBirth;

            setProfile(profileData);
        }
        fetchData();
    }, []);

    const initialValues = {
        firstName: '',
        middleName: '',
        lastName: '',
        isLinkedToAccount: '',
        dateOfBirth: '',
        accountId: ''
    }

    const handleFormSubmit = async (values, actions) => {
        setIsSubmitting(true);
        try {
            console.log(JSON.stringify(values));
            const response = await fetch(`http://localhost:7111/gateway/patients/${profile.id}`, {
                method: 'PUT',
                headers: headers,
                body: JSON.stringify(values),
            })
        } catch (error) {

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
                                Сохранить
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

export default ProfileForm;