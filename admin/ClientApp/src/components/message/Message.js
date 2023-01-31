import React from 'react';
import { Snackbar, IconButton, SnackbarContent, Typography, makeStyles } from '@material-ui/core';
import { green, amber, blue, grey } from '@material-ui/core/colors';
import { useDispatch, useSelector } from 'react-redux';
import clsx from 'clsx';
import * as Actions from '../../redux/actions/messageAction';
import CloseIcon from '@material-ui/icons/Close';
import Icons from './Icons';

const useStyles = makeStyles((theme) => ({
    root: {},
    success: {
        backgroundColor: green[600],
        color: '#FFFFFF'
    },
    error: {
        backgroundColor: grey['900'],// theme.palette.error.dark,
        color: '#FFFFFF' //theme.palette.getContrastText(theme.palette.error.dark)
    },
    info: {
        backgroundColor: blue[600],
        color: '#FFFFFF'
    },
    warning: {
        backgroundColor: amber[600],
        color: '#FFFFFF'
    },
    close: {
        padding: theme.spacing(0.5),
    },
}));


const Message = () => {
    const dispatch = useDispatch();
    const { showing, options, notificationOptions } = useSelector((stateSelector) => stateSelector.message);
    const classes = useStyles();

    return (
        <>
            <Snackbar
                {...options}
                open={showing}
                onClose={() => dispatch(Actions.hideMessage())}
                classes={{
                    root: classes.root
                }}
                ContentProps={{
                    variant: 'body2',
                    headlineMapping: {
                        body1: 'div',
                        body2: 'div'
                    }
                }}
            >
                <SnackbarContent
                    className={clsx(classes[options.variant])}
                    message={
                        <div className="flex items-center">
                            <Icons name={options.variant} />
                            <Typography style={{ color: "#FFFFFF" }} className="mx-4">{options.message}</Typography>
                        </div>
                    }
                    action={[
                        <IconButton
                            key="close"
                            aria-label="Close"
                            color="inherit"
                            onClick={() => dispatch(Actions.hideMessage())}
                        >
                            <CloseIcon />
                        </IconButton>
                    ]}
                />
            </Snackbar>
            <Snackbar
                {...notificationOptions}
                onClose={() => dispatch(Actions.hideNotification())}
                action={
                    <>
                        <IconButton
                            aria-label="close"
                            color="inherit"
                            className={classes.close}
                            onClick={() => dispatch(Actions.hideNotification())}
                        >
                            <CloseIcon />
                        </IconButton>
                    </>
                }
            />
        </>
    );
}

export default Message;