import React, { useState } from 'react';
import { Formik, Form, Field } from 'formik';
import { Link, useParams } from 'react-router-dom';
import * as Yup from 'yup';
import { Button, Grid, TextField } from '@mui/material';
import GridWrapper from '../common/GridWrapper/GridWrapper';

const validationSchema = Yup.object().shape({
    complaints: Yup.string().required(),
    conclusion: Yup.string().required(),
    recomendations: Yup.string().required(),
    diagnosis: Yup.string(),
    appoitmentId: Yup.number().required()
});

const NewResultForm = () => {
    const { id } = useParams();
    const [isSubmitting, setIsSubmitting] = useState(false);

    var accessToken = localStorage.getItem('accessToken');
    const headers = new Headers();
    headers.append('Authorization', `Bearer ${accessToken}`);
    headers.append('Content-Type', 'application/json');

    const initialValues = {
        complaints: '',
        conclusion: '',
        recomendations: '',
        diagnosis: '',
        appoitmentId: id
    }

    const handleSubmit = async (values) => {
        console.log(JSON.stringify(values));
        setIsSubmitting(true);
        try {
            await fetch('http://localhost:7111/gateway/results', {
                method: 'POST',
                headers: headers,
                body: JSON.stringify(values)
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
                        <Grid container spacing={2} justify='center' alignItems='center'>
                            <Field as={TextField} name='complaints' label='Жалобы'
                                fullWidth
                                error={formikProps.touched.complaints && !!formikProps.errors.complaints}
                                helperText={formikProps.touched.complaints && formikProps.errors.complaints}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='conclusion' label='Заключение'
                                fullWidth
                                error={formikProps.touched.conclusion && !!formikProps.errors.conclusion}
                                helperText={formikProps.touched.conclusion && formikProps.errors.conclusion}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='recomendations' label='Рекомендации'
                                fullWidth
                                error={formikProps.touched.recomendations && !!formikProps.errors.recomendations}
                                helperText={formikProps.touched.recomendations && formikProps.errors.recomendations}
                                margin='normal'
                                variant='outlined'
                            />
                            <Field as={TextField} name='diagnosis' label='Диагноз'
                                fullWidth
                                error={formikProps.touched.diagnosis && !!formikProps.errors.diagnosis}
                                helperText={formikProps.touched.diagnosis && formikProps.errors.diagnosis}
                                margin='normal'
                                variant='outlined'
                            />
                            <Button type='submit' color='primary' disabled={isSubmitting}>
                                Создать
                            </Button>
                            <Link to={'/appoitments'}>
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

export default NewResultForm;