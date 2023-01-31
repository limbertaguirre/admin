

export const verificarNumero = (data) => {    
    return (/^([0-9])*$/.test(data));         
}
export const verificaAlfanumerico = (data) => {
    var patt = new RegExp(/^[A-Za-z0-9\s]+$/g);     
    return (patt.test(data));
}

export const validarCorporativo = (data) => {
    return (data !== "" && (!/@+\/^[A-Za-z0-9\s]+/.test(data)))
}

export const validarformatoEmail = (data) => {
    return (data !== "" && (!/\S+@\S+\.\S+/.test(data)))
}