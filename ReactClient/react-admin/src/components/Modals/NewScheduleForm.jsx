import React, { useEffect, useState } from 'react';
import BasicDatePicker from '../common/DateTimePickers/DatePicker/BasicDatePicker';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import GridWrapper from '../common/GridWrapper/GridWrapper';
import { Button, TextField, Grid, Typography, FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const validationSchema = Yup.object().shape({
    doctorId: Yup.string().required(),
    doctorFirstName: Yup.string().required(),
    doctorLastName: Yup.string().required(),
    date: Yup.string().required(),
    time: Yup.string().matches(/^([01]\d|2[0-3]):([0-5]\d)$/, 'Invalid time format (hh:mm)').required(),
    isBooked: Yup.boolean().required(),
    appoitmentId: Yup.number()
});

const NewScheduleForm = () => {
    const initialValues = {
        doctorId: '',
        doctorFirstName: '',
        doctorLastName: '',
        date: '',
        time: '',
        isBooked: false,
        appoitmentId: 0
    }
    const [doctors, setDoctors] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            var response = await fetch('http://localhost:7111/gateway/doctors');
            setDoctors(await response.json());
        }

        fetchData();
    }, []);

    const handleSubmit = async (values) => {
        setIsSubmitting(true);
        values.isBooked = JSON.parse(values.isBooked);
        console.log(JSON.stringify(values));
        await fetch('http://localhost:7111/gateway/schedules', {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(values)
        });
        setIsSubmitting(false);
    }

    return (
        <>
            <GridWrapper>
                <Formik
                    initialValues={initialValues}
                    validationSchema={validationSchema}
                    onSubmit={handleSubmit}
                >
                    {(formikProps) => (
                        <Form>
                            <Grid container spacing={2} justify='center' alignItems='center'>
                                <Grid item xs={12}>
                                    <FormControl fullWidth variant='outlined'>
                                        <InputLabel htmlFor='doctorId'>Имя доктора</InputLabel>
                                        <Select
                                            id='doctorId'
                                            name='doctorId'
                                            label='Имя доктора'
                                            value={formikProps.values.doctorId}
                                            onChange={(e) => {
                                                formikProps.handleChange(e);
                                                const selectedDoctorId = e.target.value;
                                                const selectedDoctor = doctors.find(doctor => doctor.accountId === selectedDoctorId);
                                                if (selectedDoctor) {
                                                    formikProps.setFieldValue('doctorFirstName', selectedDoctor.firstName);
                                                    formikProps.setFieldValue('doctorLastName', selectedDoctor.lastName);
                                                }
                                            }}
                                            onBlur={formikProps.handleBlur}
                                            error={formikProps.touched.doctorId && !!formikProps.errors.doctorId}
                                        >
                                            <MenuItem value=''>
                                                <em>Выберите доктора</em>
                                            </MenuItem>
                                            {doctors.map((doctor) => (
                                                <MenuItem key={doctor.id} value={`${doctor.accountId}`}>
                                                    {`${doctor.firstName} ${doctor.lastName}`}
                                                </MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                    {formikProps.errors.doctorId && formikProps.touched.doctorId && (
                                        <Typography color='error'>{formikProps.errors.doctorId}</Typography>
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    <BasicDatePicker
                                        label='Дата'
                                        value={formikProps.values.date}
                                        onChange={(date) => {
                                            formikProps.setFieldValue('date', date);
                                        }}
                                    />
                                    {formikProps.errors.date && (
                                        <Typography color='error'>{formikProps.errors.date}</Typography>
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    <Field as={TextField} name='time' label='Время'
                                        fullWidth
                                        error={formikProps.touched.time && !!formikProps.errors.time}
                                        helperText={formikProps.touched.time && formikProps.errors.time}
                                        margin='normal'
                                        variant='outlined'
                                    />
                                </Grid>
                            </Grid>

                            <Button type='submit' color='primary' disabled={isSubmitting}>
                                Создать
                            </Button>
                            <Link to={'/schedules'}>
                                <Button size='small' variant='outlined'>
                                    Отмена
                                </Button>
                            </Link>
                        </Form>
                    )}
                </Formik>
            </GridWrapper>
        </>
    )
};

export default NewScheduleForm;