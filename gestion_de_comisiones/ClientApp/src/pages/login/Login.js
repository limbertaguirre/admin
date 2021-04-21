import React, { useState } from "react";
import CssBaseline from '@material-ui/core/CssBaseline';
import Link from '@material-ui/core/Link';
import Paper from '@material-ui/core/Paper';
import Box from '@material-ui/core/Box';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Button, IconButton, TextField,InputAdornment } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { useDispatch } from "react-redux";
import Visibility from "@material-ui/icons/Visibility";
import VisibilityOff from "@material-ui/icons/VisibilityOff";
import LogoSION from '../../assets/icons/LogoSION.svg';
import * as Action from '../../redux/actions/LoginAction';


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




const useStyles = makeStyles((theme) => ({
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


  


  const Login = () => {

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
          dispatch(Action.iniciarSesion());         
      };

    return (
      <Grid container component="main" className={style.root}>
        <CssBaseline />
        <Grid item xs={false} sm={4} md={7} className={style.image} />
        <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
          <div className={style.paper}>
            
            <img className={style.imagelogo} src={LogoSION} alt={'sion'}  ></img>
            <br/>
 
            <div >
             
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
              <Button
                type="submit"
                fullWidth
                variant="contained"
                color="primary"
                className={style.submit}
                onClick = {_handleRegistrar}
                disabled={!isFormValid()} 
              >
                Ingresar
              </Button>
        
              <Box mt={5}>
                <Copyright />
              </Box>
            </div>
          </div>
        </Grid>
      </Grid>
    );

  }
  
  export default Login