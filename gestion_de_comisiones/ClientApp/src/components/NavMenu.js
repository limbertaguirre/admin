import React, { useState } from 'react';
// import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap'; // al habilitar esta linea quitar la de abajo
import {Container} from 'reactstrap';
import { Link } from 'react-router-dom';
import clsx from 'clsx';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import CssBaseline from '@material-ui/core/CssBaseline';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import MailIcon from '@material-ui/icons/Mail';

import ImageIcons from '../components/ImagenIcons';
import './NavMenu.css';

const drawerWidth = 240;

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
  },
  appBar: {
    transition: theme.transitions.create(['margin', 'width'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  appBarShift: {
    width: `calc(100% - ${drawerWidth}px)`,
    marginLeft: drawerWidth,
    transition: theme.transitions.create(['margin', 'width'], {
      easing: theme.transitions.easing.easeOut,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  hide: {
    display: 'none',
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerPaper: {
    width: drawerWidth,
  },
  drawerHeader: {
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
    justifyContent: 'flex-end',
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
    transition: theme.transitions.create('margin', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    marginLeft: -drawerWidth,
  },
  contentShift: {
    transition: theme.transitions.create('margin', {
      easing: theme.transitions.easing.easeOut,
      duration: theme.transitions.duration.enteringScreen,
    }),
    marginLeft: 0,
  },
}));


export const NavMenu =(props)=> {

    const classes = useStyles();
    const [collapsed, setCollapsed] = useState(true)    
    const theme = useTheme();
    const [open, setOpen] = React.useState(false);

    const toggleNavbar= ()=> {
      setCollapsed(!collapsed);    
    }    
    const handleDrawerOpen = () => {
      setOpen(true);
    };
    const handleDrawerClose = () => {
      setOpen(false);
    };


    return (
      <header>
         <div className={classes.root}>
            <CssBaseline />
              <AppBar
                position="fixed"
                className={clsx(classes.appBar, {
                  [classes.appBarShift]: open,
                })}
              >
                <Toolbar>
                    <IconButton
                      color="inherit"
                      aria-label="open drawer"
                      onClick={handleDrawerOpen}
                      edge="start"
                      className={clsx(classes.menuButton, open && classes.hide)}
                    >
                    <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" noWrap>
                      GESTOR DE COMISONES
                    </Typography>
                </Toolbar>
              </AppBar>
            <Drawer
              className={classes.drawer}
              variant="persistent"
              anchor="left"
              open={open}
              classes={{
                paper: classes.drawerPaper,
              }}
            >
              <div className={classes.drawerHeader}>
                <IconButton onClick={handleDrawerClose}>
                  {theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
                </IconButton>
              </div>
          <Divider />
          <List>
                <ListItem button key={1}>
                    <ListItemIcon>
                    <Link to={process.env.PUBLIC_URL + "/"} >                 
                            <ImageIcons name={'home'} />
                    </Link>               
                    </ListItemIcon>            
                    <Link to={process.env.PUBLIC_URL + "/"} >
                        <ListItemText primary={"Home"} />                
                    </Link>
                </ListItem>
                <ListItem button key={2}>
                    <ListItemIcon>
                    <Link to={process.env.PUBLIC_URL + "/"}>                 
                            <ImageIcons name={'producto'} />
                    </Link>               
                    </ListItemIcon>            
                    <Link to={process.env.PUBLIC_URL + "/counter"}>
                        <ListItemText primary={'page2'} />                
                    </Link>
                </ListItem>
                <ListItem button key={3}>
                    <ListItemIcon>
                    <Link to={process.env.PUBLIC_URL + "/"}>                 
                            <ImageIcons name={'producto'} />
                    </Link>               
                    </ListItemIcon>            
                    <Link to={process.env.PUBLIC_URL + "/fetch-data"}>
                        <ListItemText primary={'Lista'} />                
                    </Link>
                </ListItem>
                <ListItem button key={4}>
                    <ListItemIcon>
                    <Link to={process.env.PUBLIC_URL + "/"}>                 
                            <ImageIcons name={'producto'} />
                    </Link>               
                    </ListItemIcon>            
                    <Link to={process.env.PUBLIC_URL + "/facturacion"}>
                        <ListItemText primary={'Facturacion'} />                
                    </Link>
                </ListItem>
          </List>
          <Divider />
            <List>
              {['config'].map((text, index) => (
                <ListItem button key={text}>
                  <ListItemIcon>{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}</ListItemIcon>
                  <ListItemText primary={text} />
                </ListItem>
              ))}
            </List>
        </Drawer>
        <main
          className={clsx(classes.content, {
            [classes.contentShift]: open,
          })}
        >
          <div className={classes.drawerHeader} />        
          <Container>
            {props.children}
          </Container>
        </main>
      </div>

        {/* <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">gestion_de_comisiones</NavbarBrand> 
            <NavbarToggler onClick={toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
                </NavItem>
              </ul>
            </Collapse>

          </Container>
        </Navbar> */}
      </header>
    );
  
}
