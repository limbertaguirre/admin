import React,{useState} from 'react';
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import ImageIcons from "../ImagenIcons";
import Collapse from '@material-ui/core/Collapse';
import ExpandMore from '@material-ui/icons/ExpandMore';
import ExpandLess from '@material-ui/icons/ExpandLess';
import { useHistory } from 'react-router-dom';


const LinkMenuSub =({subMenu})=> {
    let history = useHistory();
    const [openMenu, setOpenMenu] = useState(true);   
    const handleClickHijo = () => {
        setOpenMenu(!openMenu);
    };

    return (
      <>          
                <ListItem button onClick={handleClickHijo}>
                    <ListItemIcon style={{marginLeft:10}}> <ImageIcons name={subMenu.iconsSubMenu} /> </ListItemIcon>
                    <ListItemText primary={subMenu.titleSubMenu}   />
                    {openMenu ? <ExpandLess /> : <ExpandMore />}
                </ListItem>
                <Collapse in={openMenu} timeout="auto" unmountOnExit>
                    <List component="div" disablePadding>
                        {subMenu.listaSubMenu.map((value, index) => (            
                            <div key={index}>
                                <ListItem sx={{ pl: 6 }} button onClick={()=> history.push(value.path)} >
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
export default LinkMenuSub;