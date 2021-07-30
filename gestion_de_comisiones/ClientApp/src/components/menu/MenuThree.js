import React,{useState}  from 'react';
import {Container} from 'reactstrap';
import clsx from 'clsx';
import { makeStyles } from '@material-ui/core/styles';
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
import ImageIcons from '../ImagenIcons';
import {useSelector} from 'react-redux';
import { useHistory } from 'react-router-dom';
import LinkMenu from './LinkMenu';



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

  list: {
    width: 260,
  },
  fullList: {
    width: 'auto',
  }

}));




const  MenuThree =(props)=> {
    let history = useHistory();
    const classes = useStyles();    
    

    const {menu} = useSelector((stateSelector) =>{ return stateSelector.home});
    const cerrarSesion =()=>{
    
    }
    const [state, setState] = useState({
        top: false,
        left: false,
        bottom: false,
        right: false,
    });
    const toggleDrawer = (anchor, open) => (event) => {
      
        if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
          return;
        }
    
        setState({ ...state, [anchor]: open });
    };

return(    
     <>
            <div className={classes.root} >
                <CssBaseline />
                <AppBar position="fixed">
                    <Toolbar>
                        <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        onClick={toggleDrawer('left', true)}                      
                        edge="start"                        
                        >
                        <MenuIcon />
                        </IconButton>
                        <Typography variant="h6" noWrap>                         
                         {props.title}
                        </Typography>
                    </Toolbar>
                </AppBar>
                <Drawer anchor={'left'} open={state['left']} onClose={toggleDrawer('left', false)}>                   
                    <div
                        className={clsx(classes.list, {
                            [classes.fullList]: 'left' === 'top' || 'left' === 'bottom',
                        })}
                        role="presentation"
                        onClick={toggleDrawer('left', false)}                        
                        >
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
                            
                            <ListItem button key={2} onClick={()=> cerrarSesion()}>
                                <ListItemIcon>
                                <ImageIcons name={'salir'} />
                                </ListItemIcon>
                                <ListItemText primary={'Cerrar sesión'} />
                            </ListItem>
                        </List>                         
                       
                    </div>
               </Drawer>                       
        </div>
        <Container>
            {props.children}
        </Container>  
     </>     
)
}
export default  MenuThree;