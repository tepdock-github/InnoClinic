import React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import { modalStyles } from './styles';

const BasicModal = ({ open, onClose, title, subTitle, content, onSubmit }) => {

    return (
        <Modal open={open} onClose={onClose} >
            <Box sx={modalStyles.wrapper}>
                <Typography
                    variant="h6"
                    component="h2"
                >
                    {title}
                </Typography>
                <Typography sx={{ mt: 2 }}>
                    {subTitle}
                </Typography>
                {content}
                <Box sx={modalStyles.buttons}>
                    <Button
                        variant="contained"
                        onClick={onSubmit}
                    >
                        Submit
                    </Button>
                    <Button onClick={onClose}>Cancel</Button>
                </Box>
            </Box>
        </Modal>
    )
}

export default BasicModal;