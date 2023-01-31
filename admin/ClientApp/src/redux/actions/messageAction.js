export const HIDE_MESSAGE = 'MESSAGE/CLOSE';
export const SHOW_MESSAGE = 'MESSAGE/SHOW';
export const SHOW_NOTIFICATION = 'NOTIFICATION/SHOW';
export const HIDE_NOTIFICATION = 'NOTIFICATION/HIDE';

export function hideMessage()
{
    return {
        type: HIDE_MESSAGE
    }
}

export function showMessage(options)
{
    return {
        type: SHOW_MESSAGE,
        options
    }
}

export function hideNotification()
{
    return {
        type: HIDE_NOTIFICATION
    }
}

export function showNotification(options)
{
    return {
        type: SHOW_NOTIFICATION,
        options
    }
}
