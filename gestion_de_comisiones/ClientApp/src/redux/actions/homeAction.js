import * as Types from '../types/homeTypes';

export const loadHome= ()=>{
  return (dispatch)=>{
    //   console.log('llego al actions')
      fetch('weatherforecast').then(resp=>{
         console.log(resp)
      })
    //    const data = await response.json();
    //    console.log('data:', data);
    // dispatch({
    //   type:Types.NO_MORE_PAGE,
    // })
  }
}
