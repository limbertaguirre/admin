import React from 'react';
import {
    Button, Dialog, DialogContent, Typography,Grid, TextField,FormLabel,  FormControlLabel ,FormControl, Radio, RadioGroup, FormHelperText, Tooltip ,Zoom,
} from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import Alert from '@material-ui/lab/Alert';
import MenuItem from "@material-ui/core/MenuItem";
import MenuList from "@material-ui/core/MenuList";
//import LogoSion from '../../../../assets/icons/LogoSION.sgv'

const useStyles = makeStyles((theme) => ({
    root: {
      width: '100%',
      '& > * + *': {
        marginTop: theme.spacing(2),
      },
      display:'flex',
      alignItems:'center',
      justifyContent:'center',
      backgroundColor:'#E3F2F7',
      borderRadius: "3px",    
      marginBottom: theme.spacing(1)
    },
    divRadio: {
        width: '100%',
        '& > * + *': {
          marginTop: theme.spacing(2),
        },
        display:'flex',
        alignItems:'center',
        justifyContent:'center',
        //backgroundColor:'#E3F2F7',
        borderRadius: "3px",    
        marginBottom: theme.spacing(1)
      },
      group: {
        margin: `${theme.spacing.unit}px 0`,
      },
    botones:{
        background: "#1872b8", 
        boxShadow: '2px 4px 5px #1872b8',
        color:'white',  
        marginBottom:theme.spacing(2),
        marginTop:theme.spacing(2),
        marginRight:theme.spacing(1),
        marginLeft:theme.spacing(1),
    },
    TextFiel: {
        marginBottom: theme.spacing(1),
        marginTop: theme.spacing(1),
        marginRight:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingLeft:theme.spacing(1),
        //width:'98%'
    },
    Titulo: {
        marginBottom: theme.spacing(2),
        marginTop: theme.spacing(2),
        marginRight:theme.spacing(1),
        paddingRight:theme.spacing(1),
        paddingLeft:theme.spacing(1),
        //width:'98%'
    },
    titleDescripcion:{
      color:'red',  
  },
    
  }));


const TipoPagosModal = (props) => {
   const { open , closeHandelModal, listTipoPagos, idtipoPagoSelect, handleChangeRadio }= props;
   const classes = useStyles();

  return (
    <>
        <Dialog
            fullWidth={true}               
            open={open}
            aria-labelledby="customized-dialog-title"            
        >            
            <DialogContent>
                        <div className={classes.root}>    
                            <Typography variant="subtitle1" className={classes.Titulo} gutterBottom>
                                <b>SELECCIONAR FORMA DE PAGO </b>
                            </Typography>                                                                                              
                        </div>                       
                        <br />   
                        <Grid  container  >                                                     
                        <div className={classes.divRadio}>
                            {/* <Grid> */}
                            <FormControl component="fieldset" className={classes.formControl}>                                
                                <RadioGroup
                                    aria-label="Gender"
                                    name="gender1"
                                    className={classes.group}
                                    value={idtipoPagoSelect}
                                    onChange={handleChangeRadio}
                                    
                                >
                                  {listTipoPagos.map((valu, index) => (
                                    <div key={index}>
                                    <Grid container spacing={1}>
                                      <Grid item xs={8}> 
                                      <Tooltip disableFocusListener disableTouchListener TransitionComponent={Zoom} title={!valu.estado? valu.descripcion:''}>
                                        <FormControlLabel
                                            key={valu.idTipoPago} 
                                            value={valu.idTipoPago.toString()}
                                            control={<Radio color="primary" />}
                                            disabled={!valu.estado}
                                            label={valu.nombre}                                                                                    
                                        />
                                        </Tooltip>
                                       {/*  {!valu.estado &&   <Typography variant="caption" className={classes.titleDescripcion} display="block" gutterBottom>{valu.descripcion}</Typography>} */}                                   
                                      </Grid>
                                      <Grid item xs={4}>
                                          {valu.idTipoPago ===1 &&  <img  height="31" src={require('../../../../assets/icons/sionpay_large.png')} /> }
                                          {valu.idTipoPago ===2 &&  <img  height="45" src={require('../../../../assets/icons/tranfer.png')} /> }
                                          {valu.idTipoPago ===3 &&  <img  height="45" src={require('../../../../assets/icons/cheque.png')} /> }
                                        </Grid>
                                     </Grid>
                                     </div>
                                    ))}                           
                                </RadioGroup>
                                </FormControl>                     
                            </div>                               
                        </Grid>                
                    <Grid  container item xs={12}  justify="flex-end"  >
                        <Button  onClick={closeHandelModal} variant="contained" color="primary" className={classes.botones}  >
                            Cancelar
                        </Button>
                        <Button  onClick={closeHandelModal} variant="contained" color="primary" className={classes.botones} >
                            OK
                        </Button>
                    </Grid>
            </DialogContent>             
        </Dialog>
    </>
  );
};
export default TipoPagosModal;
