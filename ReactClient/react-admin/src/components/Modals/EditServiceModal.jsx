import React, { useEffect, useState } from 'react';
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import { Button, Grid, Select, TextField, MenuItem, FormControl, InputLabel, Typography, FormControlLabel, RadioGroup, Radio } from '@mui/material';
import GridWrapper from '../common/GridWrapper/GridWrapper';
import { useParams } from 'react-router-dom';

const validationSchema = Yup.object().shape({
    serviceName: Yup.string().required("Service Name is required"),
    price: Yup.number().required("Price is required"),
    isActive: Yup.boolean().required("IsActive is required"),
    categoryId: Yup.number().required("CategoryId is required"),
    specializationId: Yup.number().required("SpecializationId is required"),
});

const EditServiceModal = () => {
    const { id } = useParams();
    const [service, setService] = useState([]);
    const [category, setCategory] = useState([]);
    const [specialization, setSpecialization] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    var userId = localStorage.getItem('userId');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        serviceName: "",
        price: 0,
        isActive: false,
        categoryId: 0,
        specializationId: 0
    };

    useEffect(() => {
        const fetchData = async () => {
            const [respService, respCategory, respSpec] = await Promise.all([
                fetch(`http://localhost:7111/gateway/services/${id}`),
                fetch('http://localhost:7111/gateway/categories'),
                fetch('http://localhost:7111/gateway/specializations/active'),
            ])
            const category = await respCategory.json();
            const spec = await respSpec.json();
            const service = await respService.json();

            initialValues.categoryId = service.categoryId;
            initialValues.isActive = service.isActive;
            initialValues.price = service.price;
            initialValues.serviceName = service.serviceName;
            initialValues.specializationId = service.specializationId;

            setCategory(category);
            setSpecialization(spec);
            setService(service);
        }
        fetchData();
    }, [])

    const handleSubmit = async (values, { setSubmitting }) => {
        setIsSubmitting(true);
        try {
            values.isActive = JSON.parse(values.isActive);
            console.log(JSON.stringify(values));
            const response = await fetch(`http://localhost:7111/gateway/services/${id}`, {
                method: 'PUT',
                headers: headers,
                body: JSON.stringify(values),
            })
        } catch (error) {
            console.log(error);
        }
        setIsSubmitting(false);
    };

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
                            <Field as={TextField} name='serviceName' label='serviceName'
                                fullWidth
                                error={formikProps.touched.serviceName && !!formikProps.errors.serviceName}
                                helperText={formikProps.touched.serviceName && formikProps.errors.serviceName}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='price' label='price'
                                fullWidth
                                error={formikProps.touched.price && !!formikProps.errors.price}
                                helperText={formikProps.touched.price && formikProps.errors.price}
                                margin='normal'
                                variant='outlined'
                            />
                            <Grid item xs={12}>
                                <FormControl fullWidth variant='outlined'>
                                    <InputLabel htmlFor='categoryId'>Категория</InputLabel>
                                    <Select
                                        id='categoryId'
                                        name='categoryId'
                                        label='Категория'
                                        value={formikProps.values.categoryId}
                                        onChange={(e) => {
                                            formikProps.handleChange(e);
                                        }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.categoryId && !!formikProps.errors.categoryId}
                                    >
                                        {category.map((doctor) => (
                                            <MenuItem key={doctor.id} value={`${doctor.id}`}>
                                                {`${doctor.categoryName}`}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                                {formikProps.errors.categoryId && formikProps.touched.categoryId && (
                                    <Typography color='error'>{formikProps.errors.categoryId}</Typography>
                                )}
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl fullWidth variant='outlined'>
                                    <InputLabel htmlFor='specializationId'>Специализация</InputLabel>
                                    <Select
                                        id='specializationId'
                                        name='specializationId'
                                        label='Специализация'
                                        value={formikProps.values.specializationId}
                                        onChange={(e) => {
                                            formikProps.handleChange(e);
                                        }}
                                        onBlur={formikProps.handleBlur}
                                        error={formikProps.touched.specializationId && !!formikProps.errors.specializationId}
                                    >
                                        {specialization.map((doctor) => (
                                            <MenuItem key={doctor.id} value={`${doctor.id}`}>
                                                {`${doctor.specializationName}`}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                                {formikProps.errors.categoryId && formikProps.touched.categoryId && (
                                    <Typography color='error'>{formikProps.errors.categoryId}</Typography>
                                )}
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
                                Сохранить
                            </Button>
                            <Link to={'/services'}>
                                <Button size='small' variant='outlined'>
                                    Отмена
                                </Button>
                            </Link>
                        </Grid>
                    </Form>
                )}
            </Formik>
        </GridWrapper>
    );
};

export default EditServiceModal;