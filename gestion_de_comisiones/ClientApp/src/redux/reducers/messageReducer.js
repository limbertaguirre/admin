import * as Actions from '../actions/messageAction';

const defaultState = {
    showing: false,
    options: {
        anchorOrigin: {
            vertical: 'top',
            horizontal: 'center'
        },
        autoHideDuration: 6000,
        message: "Hi",
        variant: null
    },
    notificationOptions: {
        open: false,
        anchorOrigin: {
            vertical: 'bottom',
            horizontal: 'right'
        },
        autoHideDuration: 12000,
        message: "Hi",
        key: 'bottomright'
    }
};
const message = function (state = defaultState, action) {
    switch (action.type) {
        case Actions.SHOW_MESSAGE:
            {
                return {
                    showing: true,
                    options: {
                        ...state.options,
                        ...action.options
                    },
                    notificationOptions: { ...state.notificationOptions }
                };
            }
        case Actions.HIDE_MESSAGE:
            {
                return {
                    ...state,
                    showing: null
                };
            }
        case Actions.SHOW_NOTIFICATION: {
            return {
                ...state,
                notificationOptions: {
                    open: true,
                    anchorOrigin: action.options.hasOwnProperty('position') ?
                        action.options.position : state.notificationOptions.anchorOrigin,
                    autoHideDuration: action.options.hasOwnProperty('duration') ?
                        action.options.duration : state.notificationOptions.autoHideDuration,
                    message: action.options.message,
                    key: state.notificationOptions.anchorOrigin.vertical +
                        state.notificationOptions.anchorOrigin.horizontal
                }
            }
        }
        case Actions.HIDE_NOTIFICATION: {
            return {
                ...state,
                notificationOptions: {
                    ...state.notificationOptions,
                    open: false
                }
            };
        }
        default:
            {
                return { ...state };
            }
    }
};

export default message;