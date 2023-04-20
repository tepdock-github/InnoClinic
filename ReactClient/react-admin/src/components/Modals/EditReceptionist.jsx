import React, { useEffect, useState } from 'react';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import { Button, Grid, Select, TextField, MenuItem, FormControl, InputLabel } from '@mui/material';
import GridWrapper from '../common/GridWrapper/GridWrapper';

const validationSchema = Yup.object().shape({
    firstName: Yup.string().required(),
    middleName: Yup.string().required(),
    lastName: Yup.string().required(),
    officeId: Yup.string(),
    accountId: Yup.string()
});

const EditReceptionist = () => {
    const [profile, setProfile] = useState();
    const [office, setOffices] = useState([])
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    var userid = localStorage.getItem('userId');
    var role = localStorage.getItem('role');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        id: 0,
        firstName: '',
        middleName: '',
        lastName: '',
        officeId: '',
        accountId: ''
    }

    useEffect(() => {
        const fetchData = async () => {
            const [recepResp, officeResp] = await Promise.all([
                fetch(`http://localhost:7111/gateway/receptionists/account/${userid}`, { headers }),
                fetch('http://localhost:7111/gateway/offices')
            ])
            const office = await officeResp.json();
            const recep = await recepResp.json();

            initialValues.id = recep.id;
            initialValues.accountId = userid;
            initialValues.firstName = recep.firstName;
            initialValues.middleName = recep.middleName;
            initialValues.lastName = recep.lastName;
            initialValues.officeId = recep.officeId;

            setOffices(office);
            setProfile(recep);
        };
        fetchData();
    }, [])

    const handleFormSubmit = async (values) => {
        setIsSubmitting(true);
        console.log(JSON.stringify(values));
        console.log(role);
        await fetch(`http://localhost:7111/gateway/receptionists/${values.id}`, {
            method: 'PUT',
            headers: headers,
            body: JSON.stringify(values),
        })
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
                                        {office.map((off) => (
                                            <MenuItem key={off.id} value={off.id}>
                                                {off.address}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Grid>

                            <Button type='submit' color='primary' disabled={isSubmitting}>
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

}

export default EditReceptionist;