import React, { useState } from "react";
// import CssBaseline from '@material-ui/core/CssBaseline';
// import Paper from '@material-ui/core/Paper';
// import Box from '@material-ui/core/Box';
// import Grid from '@material-ui/core/Grid';

// import { Button, IconButton, TextField,InputAdornment } from "@material-ui/core";
// import { makeStyles } from "@material-ui/core/styles";

// import Visibility from "@material-ui/icons/Visibility";
// import VisibilityOff from "@material-ui/icons/VisibilityOff";
// import LogoSION from '../../assets/icons/LogoSION.svg';
import Link from '@material-ui/core/Link';
import Typography from '@material-ui/core/Typography';
import { useDispatch } from "react-redux";
import * as Action from '../../redux/actions/LoginAction';
///primer login
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
import Icon from "@material-ui/core/Icon";

// @material-ui/icons
import Email from "@material-ui/icons/Email";
import People from "@material-ui/icons/People";
// core components
import Header from "../../components/Header/Header.js";
import HeaderLinks from "../../components/Header/HeaderLinks.js";
import Footer from "../../components/Footer/Footer.js";
import GridContainer from "../../components/Grid/GridContainer.js";
import GridItem from "../../components/Grid/GridItem.js";
import Button from "../../components/CustomButtons/Button.js";
import Card from "../../components/Card/Card.js";
import CardBody from "../../components/Card/CardBody.js";
import CardHeader from "../../components/Card/CardHeader.js";
import CardFooter from "../../components/Card/CardFooter.js";
import CustomInput from "../../components/CustomInput/CustomInput";

import styles from "../../assets/jss/material-kit-react/views/loginPage";
import image from "../../assets/img/bg7.jpg";

const useStyles = makeStyles(styles);

function Copyright() {
  return (
    <Typography variant="body2" color="textSecondary" align="center">
      {'Copyright © '}
      <Link color="inherit" href="https://material-ui.com/">
        Grupo Sion
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}




// const useStyles = makeStyles((theme) => ({
//   root: {
//     height: '100vh',
//     color: theme.palette.text.primary,
//     flexGrow: 1,
//   },
//   image: {
//     backgroundImage: 'url(https://previews.123rf.com/images/macrovector/macrovector1709/macrovector170900335/85548186-network-for-professional-conceptual-composition-with-social-networking-graph-and-circle-avatars-conn.jpg)',
//     backgroundRepeat: 'no-repeat',
//     backgroundColor:
//       theme.palette.type === 'light' ? theme.palette.grey[50] : theme.palette.grey[900],
//     backgroundSize: 'cover',
//     backgroundPosition: 'center',
//   },
//   paper: {
//     margin: theme.spacing(8, 4),
//     display: 'flex',
//     flexDirection: 'column',
//     alignItems: 'center',
//   },
//   avatar: {
//     margin: theme.spacing(1),
//     backgroundColor: theme.palette.secondary.main,
//   },
//   form: {
//     width: '100%', 
//     marginTop: theme.spacing(1),
//   },
//   submit: {
//     margin: theme.spacing(3, 0, 2),
//   },
//   separador: {
//     paddingTop: theme.spacing(2),
//     paddingBottom: theme.spacing(2),
//     paddingLeft:'2px',
//     paddingRight:'2px'
//   },
//   imagelogo:{
//     width: '40%', 
//     [theme.breakpoints.down('md')]:{
//       width: '55%', 
//     }
//   },
// }));


  


  const Login = (props) => {
    //---------new
        const [cardAnimaton, setCardAnimation] = React.useState("cardHidden");
        setTimeout(function() {
            setCardAnimation("");
        }, 700);
        const classes = useStyles();
        const { ...rest } = props;
   //-------------------------logica password
    const dispatch = useDispatch();
    const style = useStyles();  
    const [carnet, setCarnet] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, onShowPassword] = useState(false);
    const [carnetError, setCarnetError] = useState(false);
    const [passwordError, setPasswordError] = useState(false);

      const handleClickShowPassword = () => {
        onShowPassword(prev => !prev);
      }
      const isValidPassword = (password) => {    
        return  password.length >= 5;
      };
      const isValidCarnet = (carnet) => {    
        return  carnet.length >= 3;
      };
      const isFormValid=()=> {
        return carnetError === false && passwordError === false;
      }
      const onChangeFormulario = (e)=>{     
          const texfiel = e.target.name;
          const value = e.target.value;
          if(texfiel === "carnet"){
            setCarnet(value);
            setCarnetError(!isValidCarnet(value));
          }
          if(texfiel === "password"){
            setPassword(value);
            setPasswordError(!isValidPassword(value));
          }          
      }
      const _handleRegistrar=()=>{         
          dispatch(Action.iniciarSesion(carnet, password));         
      };

    return (
        <div>
        <Header
          absolute
          color="transparent"
          brand="Material Kit React"
          rightLinks={<HeaderLinks />}
          {...rest}
        />
        <div
          className={classes.pageHeader}
          style={{
            backgroundImage: "url(" + image + ")",
            backgroundSize: "cover",
            backgroundPosition: "top center"
          }}
        >
          <div className={classes.container}>
            <GridContainer justify="center">
              <GridItem xs={12} sm={12} md={4}>
                <Card className={classes[cardAnimaton]}>
                  <form className={classes.form}>
                    <CardHeader color="primary" className={classes.cardHeader}>
                      <h4>Login</h4>
                      <div className={classes.socialLine}>
                        <Button
                          justIcon
                          href="#pablo"
                          target="_blank"
                          color="transparent"
                          onClick={e => e.preventDefault()}
                        >
                          <i className={"fab fa-twitter"} />
                        </Button>
                        <Button
                          justIcon
                          href="#pablo"
                          target="_blank"
                          color="transparent"
                          onClick={e => e.preventDefault()}
                        >
                          <i className={"fab fa-facebook"} />
                        </Button>
                        <Button
                          justIcon
                          href="#pablo"
                          target="_blank"
                          color="transparent"
                          onClick={e => e.preventDefault()}
                        >
                          <i className={"fab fa-google-plus-g"} />
                        </Button>
                      </div>
                    </CardHeader>
                    <p className={classes.divider}>Or Be Classical</p>
                    <CardBody>
                      <CustomInput
                        labelText="First Name..."
                        id="first"
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "text",
                          endAdornment: (
                            <InputAdornment position="end">
                              <People className={classes.inputIconsColor} />
                            </InputAdornment>
                          )
                        }}
                      />
                      <CustomInput
                        labelText="Email..."
                        id="email"
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "email",
                          endAdornment: (
                            <InputAdornment position="end">
                              <Email className={classes.inputIconsColor} />
                            </InputAdornment>
                          )
                        }}
                      />
                      <CustomInput
                        labelText="Password"
                        id="pass"
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "password",
                          endAdornment: (
                            <InputAdornment position="end">
                              <Icon className={classes.inputIconsColor}>
                                lock_outline
                              </Icon>
                            </InputAdornment>
                          ),
                          autoComplete: "off"
                        }}
                      />
                    </CardBody>
                    <CardFooter className={classes.cardFooter}>
                      <Button simple color="primary" size="lg">
                        Get started
                      </Button>
                    </CardFooter>
                  </form>
                </Card>
              </GridItem>
            </GridContainer>
          </div>
          <Footer whiteFont />
        </div>
      </div>
  


    //   <Grid container component="main" className={style.root}>
    //     <CssBaseline />
    //     <Grid item xs={false} sm={4} md={7} className={style.image} />
    //     <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
    //       <div className={style.paper}>
            
    //         <img className={style.imagelogo} src={LogoSION} alt={'sion'}  ></img>
    //         <br/>
 
    //         <div >
             
    //           <TextField
    //             variant="outlined"
    //             margin="normal"
    //             required
    //             fullWidth
    //             id="email"
    //             label="Usuario"
    //             name="carnet"
    //             autoComplete="carnet"
    //             autoFocus
    //             onChange={onChangeFormulario}
    //             error={carnetError}
    //             helperText={
    //               carnetError &&  "El carnet debe ser mayor a 3 digitos"
    //             }
    //           />

    //           <TextField
    //             variant="outlined"
    //             margin="normal"
    //             required
    //             fullWidth
    //             name="password"
    //             label="Contraseña"
    //             type={showPassword ? 'text' : 'password'}
    //             id="password"
    //             autoComplete="password"
    //             onChange={onChangeFormulario}
    //             error={passwordError}
    //             helperText={
    //               passwordError &&  "La constraseña no cumple los criterios de seguridad"
    //             }  

    //             InputProps={{ 
    //               endAdornment: (
    //                 <InputAdornment position="end">
    //                   <IconButton
    //                     aria-label="toggle password visibility"
    //                     onClick={handleClickShowPassword}
    //                   >
    //                     {showPassword ? <Visibility /> : <VisibilityOff />}
    //                   </IconButton>
    //                 </InputAdornment>
    //               )
    //             }}                          
    //           />              
    //           <Button
    //             type="submit"
    //             fullWidth
    //             variant="contained"
    //             color="primary"
    //             className={style.submit}
    //             onClick = {_handleRegistrar}
    //             disabled={!isFormValid()} 
    //           >
    //             Ingresar
    //           </Button>
        
    //           <Box mt={5}>
    //             <Copyright />
    //           </Box>
    //         </div>
    //       </div>
    //     </Grid>
    //   </Grid>

    );

  }
  
  export default Login