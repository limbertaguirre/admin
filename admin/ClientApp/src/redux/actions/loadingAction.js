import * as Types from './../types/loadingTypes';

export const loadingRequest = ()=>{
  return {
    type:Types.REQUEST_LOADING
  }
}
export const loadingEndRequest=()=>{
  return {
    type:Types.REQUEST_END
  }
}