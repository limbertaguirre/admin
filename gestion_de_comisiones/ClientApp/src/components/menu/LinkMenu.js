import React,{useState} from 'react';
import Drawer from "@material-ui/core/Drawer";
import CssBaseline from "@material-ui/core/CssBaseline";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import List from "@material-ui/core/List";
import Typography from "@material-ui/core/Typography";
import Divider from "@material-ui/core/Divider";
import IconButton from "@material-ui/core/IconButton";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import MenuIcon from "@material-ui/icons/Menu";
import ChevronLeftIcon from "@material-ui/icons/ChevronLeft";
import ChevronRightIcon from "@material-ui/icons/ChevronRight";
import InboxIcon from "@material-ui/icons/MoveToInbox";
import MailIcon from "@material-ui/icons/Mail";
import ImageIcons from "../ImagenIcons";
import Collapse from '@material-ui/core/Collapse';
import StarBorder from '@material-ui/icons/StarBorder';
import ExpandMore from '@material-ui/icons/ExpandMore';
import ExpandLess from '@material-ui/icons/ExpandLess';


const LinkMenu =({menu})=> {
  
    const{titleMenu,icons, listaMenu} = menu;
    const [openMenuPadre, setOpenMenuPadre] = useState(true);
    const [openMenu, setOpenMenu] = useState(true);
   
    const handleClickPadre = () => {
        setOpenMenuPadre(!openMenuPadre);
   };
   const handleClickHijo = () => {
    setOpenMenu(!openMenu);
   };


    return (
      <>
                    <ListItem button onClick={handleClickHijo}>
                        <ListItemIcon style={{marginLeft:10}}> <ImageIcons name={listaMenu.iconsSubMenu} /> </ListItemIcon>
                        <ListItemText primary={listaMenu.titleSubMenu}   />
                        {openMenu ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openMenu} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                         {listaMenu.listaSubMenu.map((value, index) => (            
                                <div key={index}>
                                    <ListItem sx={{ pl: 6 }} button>
                                        <ListItemIcon style={{marginLeft:10}}>                                       
                                        </ListItemIcon>
                                        <ListItemText primary={value.title} />
                                    </ListItem>
                                </div>
                         ))}                  
                        </List>
                    </Collapse>        
      </>
    );
  


}
export default LinkMenu;