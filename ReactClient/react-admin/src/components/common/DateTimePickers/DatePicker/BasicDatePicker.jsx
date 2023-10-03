import * as React from 'react';
import TextField from '@mui/material/TextField';
import moment from 'moment';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';

const BasicDatePicker = ({ label, value, onChange }) => {
  return (
    <LocalizationProvider dateAdapter={AdapterMoment}>
      <DatePicker
        label={label}
        value={value}
        onChange={(date) => {
          const dateString = moment(date).format('YYYY-MM-DD');
          onChange(dateString);
        }}
        renderInput={(params) => <TextField {...params} />}
      />
    </LocalizationProvider>
  );
}

export default BasicDatePicker;
