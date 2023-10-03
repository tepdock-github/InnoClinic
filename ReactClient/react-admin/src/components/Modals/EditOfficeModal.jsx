import React, { useEffect, useState } from 'react';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import { Button, Grid, TextField, FormControl, FormControlLabel, RadioGroup, Radio } from '@mui/material';
import GridWrapper from '../common/GridWrapper/GridWrapper';
import { useParams } from 'react-router-dom';

const validationSchema = Yup.object().shape({
    address: Yup.string().required(),
    phoneNumber: Yup.string(),
    isActive: Yup.boolean().required()
});

const EditOfficeModal = () => {
    const { id } = useParams();
    const [office, setOffice] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        phoneNumber: "",
        address: "",
        isActive: false
    };

    useEffect(() => {
        const fetchData = async () => {
            var response = await fetch(`http://localhost:7111/gateway/offices/${id}`);

            const office = await response.json();
            initialValues.address = office.address;
            initialValues.phoneNumber = office.phoneNumber;
            initialValues.isActive = office.isActive;

            setOffice(office);
        }
        fetchData();
    }, [id]);

    const handleSubmit = async (values) => {
        setIsSubmitting(true);

        values.isActive = JSON.parse(values.isActive);
        await fetch(`http://localhost:7111/gateway/offices/${id}`, {
            method: 'PUT',
            headers: headers,
            body: JSON.stringify(values),
        })

        setIsSubmitting(false)
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
                        <Grid  container spacing={2} justify="center" alignItems="center" >
                            <Field as={TextField} name="address" label="address"
                                fullWidth
                                error={formikProps.touched.address && !!formikProps.errors.address}
                                helperText = {formikProps.touched.address && formikProps.errors.address}
                                margin = "normal"
                                variant = 'outlined'
                            />
                        </Grid>
                        <Grid  container spacing={2} justify="center" alignItems="center" >
                            <Field as={TextField} name="phoneNumber" label="phoneNumber"
                                fullWidth
                                error={formikProps.touched.phoneNumber && !!formikProps.errors.phoneNumber}
                                helperText = {formikProps.touched.phoneNumber && formikProps.errors.phoneNumber}
                                margin = "normal"
                                variant = 'outlined'
                            />
                        </Grid>
                        <FormControl component="fieldset">
                            <RadioGroup
                                name="isActive"
                                value={formikProps.values.isActive}
                                onChange={formikProps.handleChange}
                            >
                                <FormControlLabel value={true} control={<Radio />} label="Активный" />
                                <FormControlLabel value={false} control={<Radio />} label="Неактивный" />
                            </RadioGroup>
                        </FormControl>
                        <Button type='submit' color='primary' disabled={isSubmitting}>
                            Создать
                        </Button>
                        <Link to={'/offices'}>
                            <Button size='small' variant='outlined'>
                                Отмена
                            </Button>
                        </Link>
                    </Form>
                )}
            </Formik>
        </GridWrapper>
    )
}

export default EditOfficeModal;