import React, { useEffect } from 'react';
import { grey } from '@material-ui/core/colors';
import { Typography, Grid } from '@material-ui/core';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardContent from '@material-ui/core/CardContent';


import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';

import { makeStyles, Theme } from '@material-ui/core/styles';
import Accordion from '@material-ui/core/Accordion';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import AcordionListModulos from './AcordionListModulos';
import EditOutlinedIcon from '@material-ui/icons/EditOutlined';
import IconButton from '@material-ui/core/IconButton';
import { useHistory } from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  iconMenu:{
    display:'flex',
    flexDirection:'column',
    alignContent:'center',
    alignItems:'center',
    justifyContent:'center',
    border:'1px solid',
    borderRadius:8,
   // borderStyle:'dashed',
    borderColor:grey[500],
    flex: '50%',
    margin:6,
    padding: 12,
    height:100,
    maxWidth:'33%',
    textDecoration:'none',
    color:grey[600],
  },
  typografia:{
    fontSize:14,
    fontWeight:'bold',
    fontStyle:'bold',
  },
  root: {
    display:'flex',
    maxWidth: '33%',
    margin:15,
    padding: 6,
   // background: "linear-gradient(90deg, #7194A4, #1872b8)",
    boxShadow: '2px 4px 5px #999',
    '&:hover': {
        border: '1px solid #252a3b',
        transform: 'scale(1.05, 1.09)',
        '& .MuiCardHeader-root': {
          backgroundColor: '#252a3b',
          color: 'white'
        },
      }
  },
  title: {
    fontSize: 14,
    //color:"#FFFFFF",
  },
  heading: {
    fontSize: theme.typography.pxToRem(15),
    flexBasis: '50%',
    flexShrink: 0,
  },
  secondaryHeading: {
    fontSize: theme.typography.pxToRem(15),
    color: theme.palette.text.secondary,
  },
  column: {
    flexBasis: '100%',
    fontSize: theme.typography.pxToRem(15),
    color: theme.palette.text.secondary,
  },
  gridEditRol:{
    display: 'flex',
    alignItems:"flex-start",
    justifyContent: 'flex-end',
  }

}));
const CardRol = ({modulo, redirecionarEditRol, actualizar})=>{
  const classes = useStyles();


  return(
    <Card className={classes.root}>
      <CardActionArea>
        <CardContent>
             <Grid container item xs={12}  >
                 <Grid item xs={10}  >
                    <Typography className={classes.title} color="textSecondary" gutterBottom>
                        ROL :
                    </Typography>
                    <Typography gutterBottom variant="h5" component="h2">
                        {modulo.nombre}
                    </Typography>
                    </Grid>
                    <Grid item xs={2} className={classes.gridEditRol}>
                      {actualizar&& 
                        <IconButton color="primary" component="span" onClick={()=>redirecionarEditRol(modulo.idRol)}>
                            <EditOutlinedIcon />
                        </IconButton>
                      }                       
                  </Grid>
            </Grid>
        </CardContent>
        <>      
             <Accordion>
                <AccordionSummary
                expandIcon={<ExpandMoreIcon />}
                aria-label="Expand"
                aria-controls="additional-actions1-content"
                id="additional-actions1-header"
                >
                    <Typography  className={classes.column}> <b>Lista de permisos </b></Typography>            
                </AccordionSummary>
                <AccordionDetails>                   
                    <List dense component="div" role="list">                
                        <AcordionListModulos listHisotrico={modulo.listModulos} />                            
                    <ListItem />
                    </List>                                        
                </AccordionDetails>
            </Accordion>
        </>
      </CardActionArea>
 
    </Card>
  );
}

export default CardRol;