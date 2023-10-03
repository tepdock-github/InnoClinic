import React, { useEffect, useState } from 'react';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import { Button, Grid, Select, TextField, MenuItem, FormControl, InputLabel, Typography, FormControlLabel, RadioGroup, Radio } from '@mui/material';
import GridWrapper from '../common/GridWrapper/GridWrapper';
import { useParams } from 'react-router-dom';

const validationSchema = Yup.object().shape({
    specializationName: Yup.string().required("Specialization Name is required"),
    isActive: Yup.boolean().required("IsActive is required"),
});

const EditSpecializations = () => {
    const { id } = useParams();
    const [specialization, setSpecialization] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        specializationName: "",
        isActive: false
    };

    const handleSubmit = async (values, { setSubmitting }) => {
        setIsSubmitting(true);
        try {
            values.isActive = JSON.parse(values.isActive);
            console.log(JSON.stringify(values));
            const response = await fetch(`http://localhost:7111/gateway/specializations/${id}`, {
                method: 'PUT',
                headers: headers,
                body: JSON.stringify(values),
            })
        } catch (error) {
            console.log(error);
        }
        setIsSubmitting(false);
    };

    useEffect(() => {
        const fetchData = async () => {
            var response = await fetch(`http://localhost:7111/gateway/specializations/${id}`);
            
            const spec = await response.json();
            initialValues.specializationName = spec.specializationName;
            initialValues.isActive = spec.isActive;

            setSpecialization(spec);
        }
        fetchData();
    }, [])

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
                            <Field as={TextField} name='specializationName' label='specializationName'
                                fullWidth
                                error={formikProps.touched.specializationName && !!formikProps.errors.specializationName}
                                helperText={formikProps.touched.specializationName && formikProps.errors.specializationName}
                                margin='normal'
                                variant='outlined'
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
                        <Link to={'/specializations'}>
                            <Button size='small' variant='outlined'>
                                Отмена
                            </Button>
                        </Link>
                    </Form>
                )}
            </Formik>
        </GridWrapper >
    )
};


export default EditSpecializations;