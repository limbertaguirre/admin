import { apiComerce } from "./gestorproyect";
import * as Actions from "./../redux/actions/messageAction";
import { loadingRequest, loadingEndRequest } from "./../redux/actions/loadingAction";

const _hanldeThen = (res, dispatch) => {
  dispatch(loadingEndRequest());
  const result = res.data;
  if (result && result.code === 0) {
      return Promise.resolve(result);
  } else if (result && result.code === 1) {    
      return Promise.resolve(result);    
  }else {
      return Promise.resolve(result);
  }
};

const _hanldeCatch = (error, dispatch) => {
  dispatch(loadingEndRequest());
  if (error.response) {
    const { status} = error.response;

    if (error.code === "ECONNABORTED") {
       
      dispatch( Actions.showMessage({ message: "Tiempo Agotado", variant: "error" }) );
      
    } else if (status === 404) {
       
      dispatch( Actions.showMessage({
          message: "Error Pagina no encontrada",
          variant: "error",
        })
      );
    } else if (!error.response) {
       
      dispatch(
        Actions.showMessage({
          message: "Verifique su conexion a internet",
          variant: "error",
        })
      );
    } else {
      dispatch(
        Actions.showMessage({
          message: "Intente nuevamente por favor",
          variant: "error",
        })
      );
    }
  } else {
    dispatch(
      Actions.showMessage({
        message: "Intente nuevamente por favor",
        variant: "error",
      })
    );
  }
  return Promise.reject(error);
};



export const requestGet = (url, data, dispatch) => {    
  dispatch(loadingRequest());
//   const token = localStorage.getItem("token");
  const headers = { ...data, headers: { ...data } };  
  return apiComerce.get(url, headers)
    .then((response) => {
      return _hanldeThen(response, dispatch);
    })
    .catch((error) => {
      return _hanldeCatch(error, dispatch);
    });
};

export const requestPost = (url, data, dispatch) => {
  dispatch(loadingRequest());
  const config = { headers: { "Content-Type": "application/json"
                          //  ,"token":token 
                 } };
  
  return apiComerce.post(url, data, config)
    .then((response) => {
      return _hanldeThen(response, dispatch);
    })
    .catch((error) => {
      return _hanldeCatch(error, dispatch);
    });
};

export const requestGetWhithHeaders = (url, data, header ,dispatch) => {
  dispatch(loadingRequest());
//   const token = localStorage.getItem("token");
  const headers = { ...data, headers: { ...header
                //   , token: token
                   } };
  return apiComerce.get(url, headers).then((response) => {
         return _hanldeThen(response, dispatch);
    })
    .catch((error) => {
          return _hanldeCatch(error, dispatch);
    });
};


export const requestPostWhithHeaders = (url, data,headerParam, dispatch) => {
  dispatch(loadingRequest());
//   const token = localStorage.getItem("token");
  const config = { headers: {...headerParam, "Content-Type": "application/json"
                //   ,"token":token 
                 } };
  return apiComerce.post(url, data, config).then((response) => {
          return _hanldeThen(response, dispatch);
    })
    .catch((error) => {
           return _hanldeCatch(error, dispatch);
    });
};