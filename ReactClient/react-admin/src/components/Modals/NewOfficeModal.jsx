import React, { useEffect, useState } from 'react';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import { Button, Grid, TextField, FormControl, InputLabel, FormHelperText, FormControlLabel, RadioGroup, Radio } from '@mui/material';
import GridWrapper from '../common/GridWrapper/GridWrapper';

const validationSchema = Yup.object().shape({
    address: Yup.string().required(),
    phoneNumber: Yup.string(),
    isActive: Yup.boolean().required()
});

const NewOfficeModal = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        address: '',
        phoneNumber: '',
        isActive: true
    }

    const handleFormSubmit = async (values, actions) => {
        console.log(values);
        setIsSubmitting(true);

        try {
            await fetch('http://localhost:7111/gateway/offices', {
                method: 'POST',
                headers: headers,
                body: JSON.stringify(values)
            })
        } catch (error) {
            console.log(error);
        }

        setIsSubmitting(false)
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
                        <Grid container spacing={2} justify="center" alignItems="center">
                            <Field as={TextField} name='address' label='Адрес'
                                fullWidth
                                error={formikProps.touched.address && !!formikProps.errors.address}
                                helperText={formikProps.touched.address && formikProps.errors.address}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='phoneNumber' label='Мобильный телефон'
                                fullWidth
                                error={formikProps.touched.phoneNumber && !!formikProps.errors.phoneNumber}
                                helperText={formikProps.touched.phoneNumber && formikProps.errors.phoneNumber}
                                margin='normal'
                                variant='outlined'
                            />
                            <FormControl component="fieldset" error={formikProps.touched.isActive && !!formikProps.errors.isActive}>
                                
                                <Field
                                    as={RadioGroup}
                                    name="isActive"
                                    aria-label="is-active"
                                    row
                                >
                                    <FormControlLabel
                                        value={true}
                                        control={<Radio />}
                                        label="Yes"
                                    />
                                    <FormControlLabel
                                        value={false}
                                        control={<Radio />}
                                        label="No"
                                    />
                                </Field>
                                {formikProps.touched.isActive && formikProps.errors.isActive && (
                                    <FormHelperText>{formikProps.errors.isActive}</FormHelperText>
                                )}
                            </FormControl>

                            <Button type='submit' color='primary' disabled={isSubmitting}>
                                Создать
                            </Button>
                            <Link to={'/offices'}>
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

export default NewOfficeModal;