import React from 'react';
import { useFormik } from 'formik';
import Grid from '@mui/material/Grid';

const validate = values => {
    const errors = {};
    if (!values.email) {
        errors.email = 'Email is required';
    } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
        errors.email = 'Invalid email address';
    }
    if (!values.password) {
        errors.password = 'Password is required';
    } else if (values.password.length <= 6 || values.password.length >= 12) {
        errors.password = 'Password incorrect length(6 min and 12 max)';
    }

    return errors;
}

const SignIn = () => {
    const formik = useFormik({
        initialValues: {
            email: "",
            password: "",
            confirmPassword: ""
        },
        validate,
        onSubmit: values => {
            JSON.stringify(values, null, 2);
        },
    });
    return (
        <Grid item xs={8} sx={{marginLeft: '320 px'}}>
            <form onSubmit={formik.handleSubmit}>
                <label htmlFor='email'>Email:</label>
                <input
                    id='email'
                    name='email'
                    type='email'
                    onChange={formik.handleChange}
                    value={formik.values.email}
                />
                {formik.errors.email ? <div>{formik.errors.email}</div> : null}

                <label htmlFor='password'>Password:</label>
                <input
                    id='password'
                    name='password'
                    type='password'
                    onChange={formik.handleChange}
                    value={formik.values.password}
                />
                {formik.errors.password ? <div>{formik.errors.password}</div> : null}

                <button type='submit'>Sign In</button>
            </form>
        </Grid>
    )
}

export default SignIn;