import React, { useEffect, useState } from 'react';
import BasicDatePicker from '../../common/DateTimePickers/DatePicker/BasicDatePicker';
import { Formik, Form } from 'formik';
import { Link, useParams } from 'react-router-dom';
import * as Yup from 'yup';
import GridWrapper from '../../common/GridWrapper/GridWrapper';
import { Button, Grid, Typography, FormControl, InputLabel, Select, MenuItem, RadioGroup, Radio, FormControlLabel } from '@mui/material';

const validationSchema = Yup.object().shape({
    doctorId: Yup.string().required('Please chose doctor'),
    patientId: Yup.string().required(),
    serviceId: Yup.string().required('Please chose service'),
    date: Yup.date().min(new Date()).required(),
    scheduleId: Yup.number().required(),
    time: Yup.string().required()
});

const ReceptionistEditAppoitmentForm = () => {
    const initialvalues = {
        doctorId: '',
        doctorFirstName: '',
        doctorLastName: '',
        patientId: '',
        patientFirstName: '',
        patientLastName: '',
        serviceId: 0,
        serviceName: '',
        date: '',
        scheduleId: 0,
        time: '',
        isApproved: false,
        isComplete: false
    }

    const { id } = useParams();
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

                const [doctorsResp, serviceResp, profileResp, appoitResp] = await Promise.all([
                    fetch('http://localhost:7111/gateway/doctors'),
                    fetch('http://localhost:7111/gateway/services/active'),
                    fetch(`http://localhost:7111/gateway/patients`, { headers }),
                    fetch(`http://localhost:7111/gateway/appoitments/${id}`, { headers })
                ]);

                if (profileResp.status === 404) {
                    alert('Заполните или создайте профиль');
                    setIsSubmitting(true);
                }

                const doctorsData = await doctorsResp.json();
                const serviceData = await serviceResp.json();
                const profileData = await profileResp.json();
                const appoitData = await appoitResp.json();

                console.log(appoitData);
                initialvalues.date = appoitData.date;
                initialvalues.isApproved = appoitData.isApproved;
                initialvalues.isComplete = appoitData.isComplete;
                initialvalues.serviceId = appoitData.serviceId;
                initialvalues.serviceName = appoitData.serviceName;
                initialvalues.doctorId = appoitData.doctorId;
                initialvalues.doctorFirstName = appoitData.doctorFirstName;
                initialvalues.doctorLastName = appoitData.doctorLastName;
                initialvalues.patientId = appoitData.patientId;
                initialvalues.patientFirstName = appoitData.patientFirstName;
                initialvalues.patientLastName = appoitData.patientLastName;
                initialvalues.scheduleId = appoitData.scheduleId;
                initialvalues.time = appoitData.time;

                console.log(JSON.stringify(initialvalues));
                setDoctors(doctorsData);
                setServices(serviceData);
                setProfile(profileData);

                fetchTimeSlots(appoitData.date, appoitData.doctorId);
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

    const handleFormSubmit = async (values,) => {
        setIsSubmitting(true);
        try {
            values.isApproved = JSON.parse(values.isApproved);
            values.isComplete = JSON.parse(values.isComplete);
            console.log(JSON.stringify(values));
            await fetch(`http://localhost:7111/gateway/appoitments/${id}`, {
                method: 'PUT',
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
                                    <InputLabel htmlFor='doctorId'>Доктор</InputLabel>
                                    <Select
                                        id='doctorId'
                                        name='doctorId'
                                        label='Доктор'
                                        value={formikProps.values.doctorId}
                                        onChange={(e) => {
                                            formikProps.handleChange(e);
                                        }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.doctorId && !!formikProps.errors.doctorId}
                                    >
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
                                    <InputLabel htmlFor='patientId'>Пациент</InputLabel>
                                    <Select
                                        id='patientId'
                                        name='patientId'
                                        label='Пациент'
                                        value={formikProps.values.patientId}
                                        onChange={(e) => {
                                            formikProps.handleChange(e);
                                        }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.patientId && !!formikProps.errors.patientId}
                                    >
                                        {profile.map((patient) => (
                                            <MenuItem key={patient.id} value={`${patient.id}`}>
                                                {`${patient.firstName} ${patient.lastName}`}
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
                                            }}
                                            onBlur={formikProps.handleBlur}
                                            error={formikProps.touched.serviceId && !!formikProps.errors.serviceId}
                                        >
                                            {services.map((service) => (
                                                <MenuItem key={service.id} value={`${service.id}`}>
                                                    {`${service.serviceName}`}
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
                                            console.log(e.target.value);
                                            const selectedScheduleId = e.target.value;
                                            const selectedSchedule = timeSlots.find(slot => slot.id === parseInt(selectedScheduleId));
                                            if(selectedSchedule){
                                                formikProps.setFieldValue('time', selectedSchedule.time)
                                            }
                                        }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.scheduleId && !!formikProps.errors.scheduleId}
                                    >
                                        {timeSlots.map((slot) => (
                                            <MenuItem key={slot.id} value={slot.id}>
                                                {`${slot.time}`}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                                {formikProps.errors.scheduleId && formikProps.touched.scheduleId && (
                                    <Typography color='error'>{formikProps.errors.scheduleId}</Typography>
                                )}
                            </Grid>
                                <FormControl component="fieldset">
                                    <RadioGroup
                                        name="isApproved"
                                        value={formikProps.values.isApproved}
                                        onChange={formikProps.handleChange}
                                    >
                                        <FormControlLabel value={true} control={<Radio />} label="isApproved" />
                                        <FormControlLabel value={false} control={<Radio />} label="is not" />
                                    </RadioGroup>
                                </FormControl>
                                <FormControl component="fieldset">
                                    <RadioGroup
                                        name="isComplete"
                                        value={formikProps.values.isComplete}
                                        onChange={formikProps.handleChange}
                                    >
                                        <FormControlLabel value={true} control={<Radio />} label="Complete" />
                                        <FormControlLabel value={false} control={<Radio />} label="Not Complete" />
                                    </RadioGroup>
                                </FormControl>
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

export default ReceptionistEditAppoitmentForm;