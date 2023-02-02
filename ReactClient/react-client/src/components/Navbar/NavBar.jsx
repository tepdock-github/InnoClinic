import * as React from 'react';
import Drawer from '@mui/material/Drawer';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import { NavbarItems } from './consts/NavBarListItems';
import { useNavigate } from 'react-router-dom';

const drawerWidth = 240;

const Navbar = () => {
    const navigate = useNavigate();

    return (
        <div>
            <Drawer
                sx={{
                    width: drawerWidth,
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: drawerWidth,
                        boxSizing: 'border-box',
                        backgroundColor: "#1C5253",
                        color: "rgba(255, 255, 255, 0.7)",
                    }
                }}
                variant="permanent"
                anchor="left"
            >
                <Toolbar />
                <Divider />
                <List>
                    {NavbarItems.map((item, index) => (
                        <ListItem 
                            key={item.id}
                            onClick={() => navigate(item.route)}>
                            <ListItemButton>
                                <ListItemIcon sx={{color: "rgba(255, 255, 255, 0.7)"}}>
                                    {item.icon}
                                </ListItemIcon>
                                <ListItemText primary={item.label} />
                            </ListItemButton>
                        </ListItem>
                    ))}
                </List>
            </Drawer>
        </div>
    );
}

export default Navbar;