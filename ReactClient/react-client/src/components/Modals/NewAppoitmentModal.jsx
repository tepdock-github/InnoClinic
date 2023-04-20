import React, { useEffect, useState } from 'react';
import BasicTimePicker from '../common/DateTimePickers/TimePicker/BasicTimePicker';
import BasicDatePicker from '../common/DateTimePickers/DatePicker/BasicDatePicker';
import { Formik, Form } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import GridWrapper from '../common/GridWrapper/GridWrapper';
import { Button, Grid, Typography, FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const validationSchema = Yup.object().shape({
    doctorId: Yup.string().required('Please chose doctor'),
    serviceId: Yup.string().required('Please chose service'),
    date: Yup.date().min(new Date()).required(),
    time: Yup.string().test('is-time', 'Time must be in HH:mm format', function (value) {
        if (!value) {
            return false;
        }
        const timeRegex = /^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$/;
        if (!timeRegex.test(value)) {
            return false;
        }
        const timeParts = value.split(':');
        const hours = parseInt(timeParts[0], 10);
        const minutes = parseInt(timeParts[1], 10);
        if (hours < 6 || hours > 22 || minutes < 0 || minutes > 59) {
            return false;
        }
        return true;
    }).required('Please enter a valid time in HH:mm format')
});

const NewAppoitmentForm = () => {
    const initialvalues = {
        doctorId: '',
        doctorFirstName: '',
        doctorLastName: '',
        patientId: '',
        patientFirstName: '',
        patientLastName: '',
        serviceId: '',
        serviceName: '',
        specializationName: '',
        date: '',
        time: '',
        isApproved: false,
        isComplete: false
    }

    const [services, setServices] = useState([]);
    const [doctors, setDoctors] = useState([]);
    const [profile, setProfile] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            try {

                const [doctorsResp, serviceResp, profileResp] = await Promise.all([
                    fetch('http://localhost:7111/gateway/doctors'),
                    fetch('http://localhost:7111/gateway/services'),
                    fetch(`http://localhost:7111/gateway/patients/account/${userId}`, { headers })
                ]);

                if(profileResp.status === 404){
                    alert('Заполните или создайте профиль для записи к врачу');
                    setIsSubmitting(true);
                }

                const doctorsData = await doctorsResp.json();
                const serviceData = await serviceResp.json();
                const profileData = await profileResp.json();

                
                setDoctors(doctorsData);
                setServices(serviceData);
                setProfile(profileData);

                initialvalues.patientId = profileData.accountId;
                initialvalues.patientFirstName = profileData.firstName;
                initialvalues.patientLastName = profileData.lastName;
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    const handleFormSubmit = async (values, actions) => {
        setIsSubmitting(true);
        try {
            console.log(JSON.stringify(values));
            const response = await fetch(`http://localhost:7111/gateway/appoitments`, {
                method: 'POST',
                headers: headers,
                body: JSON.stringify(values),
            })
        } catch (error) {

        }
        setIsSubmitting(false);
    }

    return (
        <>
            <GridWrapper>
                <Formik
                    initialValues={initialvalues}
                    validationSchema={validationSchema}
                    onSubmit={handleFormSubmit}
                >
                    {(formikProps) => (
                        <Form>
                            <Grid container spacing={2} justify="center" alignItems="center" >
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
                                                const selectedDoctor = doctors.find(doctor => doctor.id === parseInt(selectedDoctorId));
                                                if (selectedDoctor) {
                                                    formikProps.setFieldValue('doctorFirstName', selectedDoctor.firstName);
                                                    formikProps.setFieldValue('doctorLastName', selectedDoctor.lastName);
                                                    formikProps.setFieldValue('specializationName', selectedDoctor.specializationName);
                                                }
                                            }}
                                            onBlur={formikProps.handleBlur}
                                            error={formikProps.touched.doctorId && !!formikProps.errors.doctorId}
                                        >
                                            <MenuItem value=''>
                                                <em>Choose doctor</em>
                                            </MenuItem>
                                            {doctors.map((doctor) => (
                                                <MenuItem key={doctor.id} value={`${doctor.id}`}>
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
                                    <FormControl fullWidth variant='outlined'>
                                        <InputLabel htmlFor='serviceId'>Сервис</InputLabel>
                                        <Select
                                            id='serviceId'
                                            name='serviceId'
                                            label='Сервис'
                                            value={formikProps.values.serviceId}
                                            onChange={(e) => {
                                                formikProps.handleChange(e);
                                                const selectedServiceId = e.target.value;
                                                const selectedService = services.find(service => service.id === parseInt(selectedServiceId));
                                                console.log(selectedService);
                                                if (selectedService) {
                                                    formikProps.setFieldValue('serviceName', selectedService.serviceName);
                                                }
                                            }}
                                            onBlur={formikProps.handleBlur}
                                            error={formikProps.touched.serviceId && !!formikProps.errors.serviceId}
                                        >
                                            <MenuItem value=''>
                                                <em>Choose service</em>
                                            </MenuItem>
                                            {services.map((spec) => (
                                                <MenuItem key={spec.id} value={spec.id}>
                                                    {spec.serviceName}
                                                </MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                    {formikProps.errors.serviceId && formikProps.touched.serviceId && (
                                        <Typography color='error'>{formikProps.errors.serviceId}</Typography>
                                    )}
                                </Grid>

                                <Grid item xs={12}>
                                    <BasicDatePicker
                                        label='Date'
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
                                    <BasicTimePicker
                                        label='Time'
                                        value={formikProps.values.time}
                                        onChange={(time) => {
                                            formikProps.setFieldValue('time', time);
                                        }}
                                    />
                                    {formikProps.errors.time && (
                                        <Typography color='error'>{formikProps.errors.time}</Typography>
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    <Button type='submit' disabled={formikProps.isSubmitting} variant='contained' color='primary'>
                                        Submit
                                    </Button>
                                    <Link to={'/appoitments'}>
                                        <Button type='button'>
                                            Go back
                                        </Button>
                                    </Link>
                                </Grid>
                            </Grid>
                        </Form>
                    )}
                </Formik>
            </GridWrapper>
        </>
    )
}

export default NewAppoitmentForm;