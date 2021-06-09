

export const verificarAcceso = (listaPerfiles, namePagePermisoHash, history)=>{
    let perfil = listaPerfiles.filter(x => x.hash == namePagePermisoHash );
    if( perfil.length > 0){
    return true;
    } else{
        history.push('/page/sin-acceso');
    return false;
    }
}
export const validarPermiso = (listaPerfiles, namePagePermisoHash )=>{
    let perfil = listaPerfiles.filter(x => x.hash == namePagePermisoHash );
    return perfil.length > 0;
  
}