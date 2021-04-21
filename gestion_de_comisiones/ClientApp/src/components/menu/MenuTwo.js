import React,{useState}  from 'react';
import {Container} from 'reactstrap';
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
import ImageIcons from '../ImagenIcons';
import {useSelector,useDispatch} from 'react-redux';
import { useHistory } from 'react-router-dom';
import LinkMenu from './LinkMenu';
import * as ActionLogin from '../../redux/actions/LoginAction'

const drawerWidth = 270;

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


const  MenuTwo =(props)=> {
    let history = useHistory();
    const classes = useStyles();   
    const dispatch= useDispatch(); 
    const theme = useTheme();
    const [open, setOpen] = useState(false);

    const handleDrawerOpen = () => {
      setOpen(true);
    };
    const handleDrawerClose = () => {      
      setOpen(false);
    };
    const {menu} = useSelector((stateSelector) =>{ return stateSelector.home});
    const cerrarSesion =()=>{
        dispatch(ActionLogin.cerrarSesion());
    }


return(    
     <>
            <div className={classes.root} >
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
                          {props.title}
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
                    <ListItem button key={1}  onClick={()=>history.push('/')} >
                      <ListItemIcon>              
                          <ImageIcons name={"home"} />              
                      </ListItemIcon>              
                        <ListItemText primary={"Principal"} />
                      
                    </ListItem>
                
                        {menu.map((value, index) => (            
                            <div key={index}>                  
                              <LinkMenu  menu={value} />            
                            </div>
                        ))}            
            </List>
            <Divider />
                <List>
                      <ListItem button key={1} onClick={()=>history.push('/configuraciones')}>
                        <ListItemIcon>
                          <ImageIcons name={'config'} />
                        </ListItemIcon>
                        <ListItemText primary={'Configuraciones'} />
                      </ListItem>
                      <ListItem button key={2} onClick={()=> cerrarSesion()}>
                        <ListItemIcon>
                          <ImageIcons name={'salir'} />
                        </ListItemIcon>
                        <ListItemText primary={'Cerrar sesión'} />
                      </ListItem>
                </List>
            </Drawer>
            <main
            className={clsx(classes.content, {
                [classes.contentShift]: open,
            })}
            >
            <div className={classes.drawerHeader} />            
            </main>
        </div>
        <Container>
            {props.children}
        </Container>  
     </>     
)
}
export default  MenuTwo;