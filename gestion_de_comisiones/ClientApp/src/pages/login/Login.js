import React, { useState } from "react";
import { Button, IconButton, TextField } from "@material-ui/core";
import Visibility from "@material-ui/icons/Visibility";
import VisibilityOff from "@material-ui/icons/VisibilityOff";
import Link from '@material-ui/core/Link';
import Typography from '@material-ui/core/Typography';
import { useDispatch } from "react-redux";
import * as Action from '../../redux/actions/LoginAction';
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
// core components
import Header from "../../components/Header/Header.js";
import HeaderLinks from "../../components/Header/HeaderLinks.js";
import Footer from "../../components/Footer/Footer.js";
import GridContainer from "../../components/Grid/GridContainer.js";
import GridItem from "../../components/Grid/GridItem.js";
import Card from "../../components/Card/Card.js";
import CardBody from "../../components/Card/CardBody.js";
import CardHeader from "../../components/Card/CardHeader.js";
import CardFooter from "../../components/Card/CardFooter.js";

import styles from "../../assets/jss/material-kit-react/views/loginPage";
import image from "../../assets/img/bg7.jpg";
import LogoSION2 from "../../assets/icons/LogoSION2-svg.svg";

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

const useStyles2 = makeStyles((theme) => ({
  root: {
    height: '100vh',
    color: theme.palette.text.primary,
    flexGrow: 1,
  },
  image: {
    backgroundImage: 'url(https://previews.123rf.com/images/macrovector/macrovector1709/macrovector170900335/85548186-network-for-professional-conceptual-composition-with-social-networking-graph-and-circle-avatars-conn.jpg)',
    backgroundRepeat: 'no-repeat',
    backgroundColor:
      theme.palette.type === 'light' ? theme.palette.grey[50] : theme.palette.grey[900],
    backgroundSize: 'cover',
    backgroundPosition: 'center',
  },
  paper: {
    margin: theme.spacing(8, 4),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: '100%', 
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  separador: {
    paddingTop: theme.spacing(2),
    paddingBottom: theme.spacing(2),
    paddingLeft:'2px',
    paddingRight:'2px'
  },
  imagelogo:{
    width: '40%', 
    [theme.breakpoints.down('md')]:{
      width: '55%', 
    }
  },
}));


  


  const Login = (props) => {  
        const [cardAnimaton, setCardAnimation] = React.useState("cardHidden");
        setTimeout(function() {
            setCardAnimation("");
        }, 700);
        const classes = useStyles();
        const { ...rest } = props;
   //-------------------------logica password
    const dispatch = useDispatch();
    const style = useStyles2();  
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
        return isValidCarnet(carnet) && isValidPassword(password);
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
          brand="Gestion de calidad"
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
          <div className={classes.container} style={{width:'100%'}}>
            <GridContainer justify="center">
              <GridItem xs={12} sm={12} md={4}>
                <Card className={classes[cardAnimaton]}>
                  <div className={classes.form}>                  

                    <CardHeader color="info" className={classes.cardHeader} >
                     <img src={LogoSION2} alt={'sion'} style={{width:'35%'}} />
                      
                      <div >
                        <br/>
                      <h4>Iniciar Sesion</h4>  
                   
                      </div>
                    </CardHeader>                     
                    <CardBody>
                            <TextField
                                  variant="outlined"
                                  margin="normal"
                                  required
                                  fullWidth
                                  id="email"
                                  label="Usuario"
                                  name="carnet"
                                  autoComplete="carnet"
                                  autoFocus
                                  onChange={onChangeFormulario}
                                  error={carnetError}
                                  helperText={
                                    carnetError &&  "El carnet debe ser mayor a 3 digitos"
                                  }
                                />

                                <TextField
                                  variant="outlined"
                                  margin="normal"
                                  required
                                  fullWidth
                                  name="password"
                                  label="Contraseña"
                                  type={showPassword ? 'text' : 'password'}
                                  id="password"
                                  autoComplete="password"
                                  onChange={onChangeFormulario}
                                  error={passwordError}
                                  helperText={
                                    passwordError &&  "La constraseña no cumple los criterios de seguridad"
                                  }  

                                  InputProps={{ 
                                    endAdornment: (
                                      <InputAdornment position="end">
                                        <IconButton
                                          aria-label="toggle password visibility"
                                          onClick={handleClickShowPassword}
                                        >
                                          {showPassword ? <Visibility /> : <VisibilityOff />}
                                        </IconButton>
                                      </InputAdornment>
                                    )
                                  }}                          
                                />              
                    </CardBody>
                    <CardFooter className={classes.cardFooter}>
                        <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        // color="primary"
                        className={style.submit}
                        onClick = {_handleRegistrar}
                        disabled={!isFormValid()} 
                      >
                        Ingresar
                      </Button>                  
                    </CardFooter>
                    <Copyright />
                    <br />
                  </div>
                </Card>
              </GridItem>
            </GridContainer>
          </div>
          <Footer whiteFont />
        </div>
      </div>

    );

  }
  
  export default Login