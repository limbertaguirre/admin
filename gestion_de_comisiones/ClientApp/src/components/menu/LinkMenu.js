import React,{useState} from 'react';
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import ImageIcons from "../ImagenIcons";
import Collapse from '@material-ui/core/Collapse';
import ExpandMore from '@material-ui/icons/ExpandMore';
import ExpandLess from '@material-ui/icons/ExpandLess';
import LinkMenuSub from './LinkMenuSub';

const LinkMenu =({menu})=> {    
    const{titleMenu,iconMenu, listaMenu} = menu;
    const [openMenuPadre, setOpenMenuPadre] = useState(true);       
    const handleClickPadre = () => {
        setOpenMenuPadre(!openMenuPadre);
    };

    return (
      <>
        <ListItem button onClick={handleClickPadre} >
                <ListItemIcon >  <ImageIcons name={iconMenu} /> </ListItemIcon>                                
                <ListItemText primary={titleMenu} />
                {openMenuPadre ? <ExpandLess /> : <ExpandMore />}
        </ListItem>
        <Collapse in={openMenuPadre} timeout="auto" unmountOnExit>
            {listaMenu.map((listhijo,index) => ( 
                <div key={index}>
                    <LinkMenuSub  subMenu={listhijo} />        
                </div>
            ))}
        </Collapse>     
      </>
    );  
}
export default LinkMenu;