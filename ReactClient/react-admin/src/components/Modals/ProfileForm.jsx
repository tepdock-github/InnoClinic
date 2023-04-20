import React, { useEffect, useState } from 'react';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import { Button, Grid, Select, TextField, MenuItem, FormControl, InputLabel } from '@mui/material';
import GridWrapper from '../common/GridWrapper/GridWrapper';

const validationSchema = Yup.object().shape({
    firstName: Yup.string().required(),
    middleName: Yup.string(),
    lastName: Yup.string().required(),
    dateOfBirth: Yup.date().required('Дата рождения обязательное поле(YYYY-MM-DD)'),
    isLinkedToAccount: Yup.bool().required(),
    accountId: Yup.string().required(),
    careerStartYear: Yup.date().required(),
    status: Yup.string().required()
});

const ProfileForm = () => {
    const [profile, setProfile] = useState();
    const [offices, setOffices] = useState([])
    const [specialization, setSpecialization] = useState([]);

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            const [respProfile, officeResp, specResp] = await Promise.all([
                fetch(`http://localhost:7111/gateway/doctors/account/${userId}`, { headers }),
                fetch('http://localhost:7111/gateway/offices'),
                fetch('http://localhost:7111/gateway/specializations/active')
            ])
            const profileData = await respProfile.json();
            const specData = await specResp.json();
            const officeData = await officeResp.json();

            initialValues.firstName = profileData.firstName;
            initialValues.middleName = profileData.middleName;
            initialValues.lastName = profileData.lastName;
            initialValues.accountId = profileData.accountId;
            initialValues.isLinkedToAccount = profileData.isLinkedToAccount;
            initialValues.dateOfBirth = profileData.dateOfBirth;
            initialValues.careerStartYear = profileData.careerStartYear;
            initialValues.specializationName = profileData.specializationName;
            initialValues.status = profileData.status;
            initialValues.specializationId = profileData.specializationId;

            setProfile(profileData);
            setSpecialization(specData);
            setOffices(officeData);
        }
        fetchData();
    }, []);

    const initialValues = {
        id: 0,
        firstName: '',
        middleName: '',
        lastName: '',
        isLinkedToAccount: true,
        dateOfBirth: '',
        accountId: userId,
        specializationName: '',
        specializationId: 0,
        careerStartYear: '',
        status: ''
    }

    const handleSubmit = async (values, actions) => {
        console.log(values.id);
        console.log(JSON.stringify(values));
            await fetch(`http://localhost:7111/gateway/doctors/${values.id}`, {
                method: 'PUT',
                headers: headers,
                body: JSON.stringify(values),
            })
    }

    return (
        <GridWrapper>
            <Formik
                initialValues={initialValues}
                validationSchema={validationSchema}
                onSubmit={handleSubmit}
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
                            <Field as={TextField} name='careerStartYear' label='careerStartYear'
                                fullWidth
                                error={formikProps.touched.careerStartYear && !!formikProps.errors.careerStartYear}
                                helperText={formikProps.touched.careerStartYear && formikProps.errors.careerStartYear}
                                margin='normal'
                                variant='outlined'
                            />
                            <Grid item xs={12}>
                                <FormControl fullWidth variant='outlined'>
                                    <InputLabel htmlFor='officeId'>Выберите офис</InputLabel>
                                    <Select
                                        fullWidth
                                        id='officeId'
                                        name='officeId'
                                        label='Офис'
                                        value={formikProps.values.officeId}
                                        onChange={(e) => { formikProps.handleChange(e) }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.officeId && !!formikProps.errors.officeId}
                                    >
                                        <MenuItem value=''>
                                            <em>Choose office</em>
                                        </MenuItem>
                                        {offices.map((off) => (
                                            <MenuItem key={off.id} value={off.id}>
                                                {off.address}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl fullWidth variant='outlined'>
                                    <InputLabel >Специализация</InputLabel>
                                    <Select
                                        fullWidth
                                        id='specializationId'
                                        name='specializationId'
                                        label='Специализация'
                                        value={formikProps.values.specializationId}
                                        onChange={(e) => {
                                            formikProps.handleChange(e);
                                            const selectedSpecId = e.target.value;
                                            const selectedSpec = specialization.find(spec => spec.id === parseInt(selectedSpecId));
                                            console.log(selectedSpec);
                                            if (selectedSpec) {
                                                formikProps.setFieldValue('specializationName', selectedSpec.specializationName)
                                            }
                                        }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.specializationId && !!formikProps.errors.specializationId}>
                                        <MenuItem value=''>
                                            <em>Choose специализацию</em>
                                        </MenuItem>
                                        {specialization.map((spec) => (
                                            <MenuItem key={spec.id} value={spec.id}>
                                                {spec.specializationName}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl fullWidth variant='outlined'>
                                    <InputLabel htmlFor='status'>Выберите статус</InputLabel>
                                    <Select
                                        fullWidth
                                        id='status'
                                        name='status'
                                        label='Офис'
                                        value={formikProps.values.status}
                                        onChange={(e) => { formikProps.handleChange(e) }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.status && !!formikProps.errors.status}
                                    >
                                        <MenuItem value=''>
                                            <em>Выберите статус</em>
                                        </MenuItem>
                                        <MenuItem value='На работе'>
                                            <em>На работе</em>
                                        </MenuItem>
                                        <MenuItem value='Больничный'>
                                            <em>Больничный</em>
                                        </MenuItem>
                                        <MenuItem value='В отпуске'>
                                            <em>В отпуске</em>
                                        </MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>


                            <Button type='submit' color='primary'>
                                Save
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