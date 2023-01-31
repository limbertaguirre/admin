import React, { useEffect } from 'react';
import { useTimer } from 'react-timer-hook';
import moment from "moment"
import { useSelector,useDispatch } from "react-redux";
import * as Action from '../../redux/actions/LoginAction';
import Alert from "@material-ui/lab/Alert";
import AlertTitle from "@material-ui/lab/AlertTitle";
import { Typography } from '@material-ui/core';




function MyTimer({ expiryTimestamp }) {
    const dispatch = useDispatch();
    const inicializarBloqueoUsuario =()=>{
        dispatch(Action.inicializarBloquedo());
    }
    const {isBloqueado, tiempoBloqueo} = useSelector((stateSelector) =>{ return stateSelector.load});

    expiryTimestamp = (isBloqueado)? expiryTimestamp: tiempoBloqueo;
    const {seconds,minutes,hours,days,isRunning,start,pause,resume,restart,} = useTimer({ 
        expiryTimestamp: moment().add(expiryTimestamp, "seconds").toDate(), 
        autoStart:true,
        onExpire: inicializarBloqueoUsuario
    });
    useEffect(() => {
        restart(moment().add(expiryTimestamp, "seconds").toDate(), true);
    }, [expiryTimestamp]);

    return (
        <Alert icon={false} severity="warning"   >
            <Typography variant="caption" display="block" gutterBottom>
                Su usuario está bloqueado por superar el límite máximo de intentos,
                <strong>
                    {(minutes <=9)?<span> 0{minutes}</span>:<span>{minutes}</span>}
                    :
                    {(seconds<= 9)?<span> 0{seconds}</span>:<span>{seconds}</span> }
                </strong> tiempo restante.
            </Typography>
        </Alert>
    );
}



const UsuarioBloqueado = () => {
    const {isBloqueado, tiempoBloqueo} = useSelector((stateSelector) =>{ return stateSelector.load});
    const dispatch = useDispatch();
    useEffect(()=>{
        dispatch(Action.inicializarBloquedo());
    },[])
    return (
        (isBloqueado)?
        <div>
            <MyTimer expiryTimestamp={tiempoBloqueo} />
        </div>:<div></div>
    );

}
export default UsuarioBloqueado;