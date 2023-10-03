import React, { useEffect, useState } from 'react';
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
    scheduleId: Yup.number().required(),
    time: Yup.string().required()
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
        scheduleId: 0,
        time: '',
        isApproved: false,
        isComplete: false
    }

    const [services, setServices] = useState([]);
    const [doctors, setDoctors] = useState([]);
    const [profile, setProfile] = useState([]);
    const [timeSlots, setTimeSlots] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    useEffect(() => {
        const fetchData = async () => {
            try {

                const [doctorsResp, serviceResp, profileResp] = await Promise.all([
                    fetch('http://localhost:7111/gateway/doctors'),
                    fetch('http://localhost:7111/gateway/services/active'),
                    fetch(`http://localhost:7111/gateway/patients`, { headers })
                ]);

                if (profileResp.status === 404) {
                    alert('Заполните или создайте профиль');
                    setIsSubmitting(true);
                }

                const doctorsData = await doctorsResp.json();
                const serviceData = await serviceResp.json();
                const profileData = await profileResp.json();


                setDoctors(doctorsData);
                setServices(serviceData);
                setProfile(profileData);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, []);

    const fetchTimeSlots = async (date, doctorId) => {
        try {
            const response = await fetch(`http://localhost:7111/gateway/schedules/free/doctor/${doctorId}/date/${date}`, {
                headers: headers
            });

            if (response.ok) {
                const data = await response.json();
                console.log(data);
                setTimeSlots(data);
            } else {
                console.error('Error fetching time slots:', response.statusText);
            }
        } catch (error) {
            console.error('Error fetching time slots:', error);
        }
    }

    const handleFormSubmit = async (values) => {
        setIsSubmitting(true);
        console.log(JSON.stringify(values));
        await fetch(`http://localhost:7111/gateway/appoitments`, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(values),
        })
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
                                                const selectedDate = formikProps.values.date;
                                                fetchTimeSlots(selectedDate, selectedDoctorId);
                                            }}
                                            onBlur={formikProps.handleBlur}
                                            error={formikProps.touched.doctorId && !!formikProps.errors.doctorId}
                                        >
                                            <MenuItem value=''>
                                                <em>Выберите доктора</em>
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
                                        <InputLabel htmlFor='patientId'>Имя пациента</InputLabel>
                                        <Select
                                            id='patientId'
                                            name='patientId'
                                            label='Имя пациента'
                                            value={formikProps.values.patientId}
                                            onChange={(e) => {
                                                formikProps.handleChange(e);
                                                const selectedDoctorId = e.target.value;
                                                const selectedDoctor = profile.find(doctor => doctor.id === parseInt(selectedDoctorId));
                                                if (selectedDoctor) {
                                                    formikProps.setFieldValue('patientFirstName', selectedDoctor.firstName);
                                                    formikProps.setFieldValue('patientLastName', selectedDoctor.lastName);
                                                }
                                            }}
                                            onBlur={formikProps.handleBlur}
                                            error={formikProps.touched.patientId && !!formikProps.errors.patientId}
                                        >
                                            <MenuItem value=''>
                                                <em>Выберите пациента</em>
                                            </MenuItem>
                                            {profile.map((doctor) => (
                                                <MenuItem key={doctor.id} value={`${doctor.id}`}>
                                                    {`${doctor.firstName} ${doctor.lastName}`}
                                                </MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                    {formikProps.errors.patientId && formikProps.touched.patientId && (
                                        <Typography color='error'>{formikProps.errors.patientId}</Typography>
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
                                        label='Дата'
                                        value={formikProps.values.date}
                                        onChange={(date) => {
                                            formikProps.setFieldValue('date', date);
                                            const selectedDoctorId = formikProps.values.doctorId;
                                            fetchTimeSlots(date, selectedDoctorId);
                                        }}
                                        error={formikProps.touched.date && !!formikProps.errors.date}
                                    />
                                    {formikProps.errors.date && (
                                        <Typography color='error'>{formikProps.errors.date}</Typography>
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    <FormControl fullWidth variant='outlined'>
                                        <InputLabel htmlFor='scheduleId'>Время</InputLabel>
                                        <Select
                                            id='scheduleId'
                                            name='scheduleId'
                                            label='Время'
                                            value={formikProps.values.scheduleId}
                                            onChange={(e) => {
                                                formikProps.handleChange(e);
                                                const selectedSchedule = timeSlots.find(schedule => schedule.id === parseInt(e.target.value));
                                                if(selectedSchedule) {
                                                    formikProps.setFieldValue('time', selectedSchedule.time)
                                                } 
                                            }}
                                            onBlur={formikProps.handleBlur}
                                            error={formikProps.touched.scheduleId && !!formikProps.errors.scheduleId}
                                        >
                                            <MenuItem value=''>
                                                <em>Выберите время</em>
                                            </MenuItem>
                                            {timeSlots.map((schedule) => (
                                                <MenuItem key={schedule.id} value={schedule.id}>
                                                    {schedule.time}
                                                </MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                    {formikProps.errors.time && formikProps.touched.time && (
                                        <Typography color='error'>{formikProps.errors.time}</Typography>
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    <Button type='submit' disabled={formikProps.isSubmitting} variant='contained' color='primary'>
                                        Создать
                                    </Button>
                                    <Link to={'/appoitments'}>
                                        <Button type='button'>
                                            Назад
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