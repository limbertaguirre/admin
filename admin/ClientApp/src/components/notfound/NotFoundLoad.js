import React  from 'react';
import { Container, Typography, Button, makeStyles } from '@material-ui/core';
import { NavLink } from 'react-router-dom';

const useStyles = makeStyles(theme => ({
    title: {
        textAlign: 'center',
        fontSize: 140,
        fontWeight: 'bold',
        marginBlock: 16,
        color: '#2E3B55'
    },
    subtitle: {
        marginBottom: 32,
        textAlign: 'center',
    },
    buttonContainer: {
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'center', 
        alignItems: 'center'
    },
    buttonNavLink: {
        textAlign: 'center', 
        color: 'white', 
        textDecoration: 'none'
    }
}))
const  NotFoundLoad = () => {
    const styles = useStyles();       
    return (
        <Container>
            <Typography className={styles.title} component="h1" variant="h1">
                404
            </Typography>
            <Typography className={styles.subtitle} component="h4" variant="h4">
                Página no encontrada
            </Typography>
            <div className={styles.buttonContainer}>
            <Button component={NavLink} to="/"  color="secondary" variant="contained" className={styles.buttonNavLink}>
                Volver a la página principal
            </Button>
            </div>
        </Container>
    );
}
export default  NotFoundLoad;

