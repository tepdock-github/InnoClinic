import React, {useState, useEffect} from 'react';
import Grid  from '@mui/material/Grid';
import { Outlet } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import Navbar from './components/Navbar/NavBar';
import Header from './components/Header/Header';

function App() {
const [title, setTitle] = useState(null);
const location = useLocation();

useEffect(()=>{
  const parsedTitle = location.pathname.replace(/\W/g, ' ');
  setTitle(parsedTitle);
}, [location])

  return (
    <Grid container>
      <Navbar />
      <Header title={title}/>
      <Outlet />
    </Grid>
  );
}

export default App;