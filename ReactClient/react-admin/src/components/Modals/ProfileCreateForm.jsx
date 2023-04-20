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
    isLinkedToAccount: Yup.boolean().required(),
    accountId: Yup.string().required(),
    careerStartYear: Yup.string().required(),
    status: Yup.string().required()
});

const DoctorProfileCreateForm = () => {
    const [offices, setOffices] = useState([])
    const [specialization, setSpecialization] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    var userid = localStorage.getItem('userId');
    var role = localStorage.getItem('role');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        firstName: '',
        middleName: '',
        lastName: '',
        isLinkedToAccount: true,
        dateOfBirth: '',
        accountId: userid,
        specializationName: '',
        specializationId: 0,
        careerStartYear: '',
        status: 'На работе'
    }

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [officeResp, specResp] = await Promise.all([
                    fetch('http://localhost:7111/gateway/offices'),
                    fetch('http://localhost:7111/gateway/specializations/active')
                ])

                const officeData = await officeResp.json();
                const specData = await specResp.json();

                setOffices(officeData);
                setSpecialization(specData);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [])

    const handleFormSubmit = async (values, actions) => {
        setIsSubmitting(true);
        try {
            values.isLinkedToAccount = JSON.parse(values.isLinkedToAccount);
            console.log(JSON.stringify(values));
            console.log(role);
            await fetch(`http://localhost:7111/gateway/doctors`, {
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
                                        onChange={(e) => { formikProps.handleChange(e); formikProps.setFieldValue('officeId', e.target.value); }}
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

export default DoctorProfileCreateForm;