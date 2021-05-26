import React, { useEffect } from 'react';
import { grey } from '@material-ui/core/colors';
import { Typography, Grid } from '@material-ui/core';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Button from '@material-ui/core/Button';

import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Divider from '@material-ui/core/Divider';

import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Accordion from '@material-ui/core/Accordion';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import AcordionListModulos from './AcordionListModulos';
import EditOutlinedIcon from '@material-ui/icons/EditOutlined';

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
    margin:6,
    padding: 6,
  },
  title: {
    fontSize: 14,
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
const CardRol = ({modulo})=>{
  const classes = useStyles();
  useEffect(()=>{
   //  console.log('modulos', modulo.listModulos);
  },[])

  return(
    <Card className={classes.root}>
      <CardActionArea>

        <CardContent>
         
             <Grid container item xs={12}  >
                 <Grid item xs={10}  >
                    <Typography className={classes.title} color="textSecondary" gutterBottom>
                        MODULO :
                    </Typography>
                    <Typography gutterBottom variant="h5" component="h2">
                        {modulo.nombre}
                    </Typography>
                    </Grid>
                    <Grid item xs={2} className={classes.gridEditRol}>
                      <EditOutlinedIcon />
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
                    <Typography  className={classes.column}>Lista de permisos</Typography>            
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