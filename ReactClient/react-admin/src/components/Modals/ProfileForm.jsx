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
    profileId: Yup.number().required(),
    isLinkedToAccount: Yup.bool().required(),
    accountId: Yup.string().required(),
    dateOfBirth: Yup.date().required('Дата рождения обязательное поле(YYYY-MM-DD)'),
    careerStartYear: Yup.date().required('Дата начала работы обязательное поле(YYYY-MM-DD)'),
    specializationName: Yup.string().required(),
    specializationId: Yup.number().required(),
    officeId: Yup.string().required(),
    status: Yup.string().required()
});

const ProfileForm = () => {
    const [profile, setProfile] = useState();
    const [specialization, setSpecialization] = useState([]);
    const [office, setOffice] = useState([]);
    var userid = localStorage.getItem('userId');
    var accessToken = localStorage.getItem('accessToken');

    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        firstName: '',
        middleName: '',
        lastName: '',
        profileId: 0,
        isLinkedToAccount: true,
        accountId: userid,
        dateOfBirth: '',
        careerStartYear: '',
        specializationName: '',
        specializationId: 0,
        officeId: 0,
        status: ''
    }
    const handleFormSubmit = async (values) => {
        console.log(values);
        await fetch(`http://localhost:7111/gateway/doctors/${values.profileId}`, {
                method: 'PUT',
                headers: headers,
                body: JSON.stringify(values),
            })
    }

    useEffect(() => {
        const fetchData = async () => {
            const [specializationResp, officeResp, profileResp] = await Promise.all([
                fetch('http://localhost:7111/gateway/specializations/active'),
                fetch('http://localhost:7111/gateway/offices'),
                fetch(`http://localhost:7111/gateway/doctors/account/${userid}`, { headers })
            ])         
            const profileData = await profileResp.json();

            initialValues.firstName = profileData.firstName;
            initialValues.middleName = profileData.middleName;
            initialValues.lastName = profileData.lastName;
            initialValues.profileId = profileData.id;
            initialValues.dateOfBirth = profileData.dateOfBirth;
            initialValues.careerStartYear = profileData.careerStartYear;
            initialValues.specializationName = profileData.specializationName;
            initialValues.specializationId = profileData.specializationId;
            initialValues.officeId = profileData.officeId;
            initialValues.status = profileData.status;

            setSpecialization(await specializationResp.json());
            setOffice(await officeResp.json());
            setProfile(profileData);
        }

        fetchData();
    }, []);

    return (
        <GridWrapper>
            <Formik
                initialValues={initialValues}
                validationSchema={validationSchema}
                onSubmit={handleFormSubmit}
            >
                {(formikProps) => (
                    <Form>
                        <Grid container spacing={2} justify='center' alignItems='center'>
                            <Field as={TextField} name='firstName' label='Имя'
                                fullWidth
                                error={formikProps.touched.firstName && !!formikProps.errors.firstName}
                                helperText={formikProps.touched.firstName && formikProps.errors.firstName}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='middleName' label='Отчество'
                                fullWidth
                                error={formikProps.touched.middleName && !!formikProps.errors.middleName}
                                helperText={formikProps.touched.middleName && formikProps.errors.middleName}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='lastName' label='Фамилия'
                                fullWidth
                                error={formikProps.touched.lastName && !!formikProps.errors.lastName}
                                helperText={formikProps.touched.lastName && formikProps.errors.lastName}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='dateOfBirth' label='Дата рождения'
                                fullWidth
                                error={formikProps.touched.dateOfBirth && !!formikProps.errors.dateOfBirth}
                                helperText={formikProps.touched.dateOfBirth && formikProps.errors.dateOfBirth}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='careerStartYear' label='Дата начала работы'
                                fullWidth
                                error={formikProps.touched.careerStartYear && !!formikProps.errors.careerStartYear}
                                helperText={formikProps.touched.careerStartYear && formikProps.errors.careerStartYear}
                                margin='normal'
                                variant='outlined'
                            />
                            <Grid item xs={12}>
                                <FormControl fullWidth variant='outlined'>
                                    <InputLabel>Специализация</InputLabel>
                                    <Select
                                        fullWidth
                                        id='specializationId'
                                        name='specializationId'
                                        label='Специализация'
                                        value={formikProps.values.specializationId}
                                        onChange={(e) => {
                                            formikProps.handleChange(e);
                                            const selectedSpecId = e.target.value;
                                            const selectedSpec = specialization.find(s => s.id === parseInt(selectedSpecId));
                                            console.log("selected spec:" + selectedSpec.specializationName);
                                            formikProps.setFieldValue('specializationName', selectedSpec.specializationName);
                                        }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.specializationId && !!formikProps.errors.specializationId}
                                    >
                                        <MenuItem value=''>
                                            <em>Специализация</em>
                                        </MenuItem>
                                        {specialization.map((s) => (
                                            <MenuItem key={s.id} value={s.id}>
                                                {s.specializationName}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl fullWidth variant='outlined'>
                                    <InputLabel htmlFor='officeId'>Офис</InputLabel>
                                    <Select
                                        fullWidth
                                        id='officeId'
                                        name='officeId'
                                        label='Офис'
                                        value={formikProps.values.officeId}
                                        onChange={(e) => { formikProps.handleChange(e); console.log(formikProps.values.officeId); }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.officeId && !!formikProps.errors.officeId}
                                    >
                                        <MenuItem value=''>
                                            <em>Офис</em>
                                        </MenuItem>
                                        {office.map((off) => (
                                            <MenuItem key={off.id} value={off.id}>
                                                {off.address}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl fullWidth variant='outlined'>
                                    <InputLabel htmlFor='status'>Статус</InputLabel>
                                    <Select
                                        fullWidth
                                        id='status'
                                        name='status'
                                        label='Статус'
                                        value={formikProps.values.status}
                                        onChange={(e) => { formikProps.handleChange(e) }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.status && !!formikProps.errors.status}
                                    >
                                        <MenuItem value=''>
                                            <em>Статус</em>
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