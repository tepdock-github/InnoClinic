import React from 'react';
import { useFormik } from 'formik';

const validate = values => {
    const errors = {};
    if (!values.email) {
        errors.email = 'Email is required';
    } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)){
        errors.email = 'Invalid email address';
    }
    if(!values.password){
        errors.password = 'Password is required';
    } else if (values.password.length <= 6 || values.password.length >= 12) {
        errors.password = 'Password incorrect length(6 min and 12 max)';
    }
    if(!values.confirmPassword){
        errors.confirmPassword = 'Confirm password is required';
    } else if (values.confirmPassword.length <= 6 || values.confirmPassword.length >= 12) {
        errors.confirmPassword = 'Password incorrect length(6 min and 12 max)';
    } else if (values.confirmPassword.valueOf() !== values.password.valueOf()) {
        errors.confirmPassword = 'Passwords isnt muching';
    }

    return errors;
}

const SignUp = () => {
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

            <label htmlFor='confirmPassword'>Confirm password:</label>
            <input
                id='confirmPassword'
                name='confirmPassword'
                type='password'
                onChange={formik.handleChange}
                value={formik.values.confirmPassword}
            />
            {formik.errors.confirmPassword ? <div>{formik.errors.confirmPassword}</div> : null}

            <button type='submit'>Sign Up</button>
        </form>
    )
}

export default SignUp;