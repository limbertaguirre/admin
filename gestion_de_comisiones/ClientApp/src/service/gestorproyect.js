import axios from "axios";
const apiUrl = "";


export const apiComerce = axios.create({
  baseURL: apiUrl,
  timeout: 90000,
  headers: {
    Authorization: "",
  },
});

apiComerce.interceptors.request.use(
  function (config) {
    return config;
  },
  function (error) {    
    return Promise.reject(error);
  }
);

apiComerce.interceptors.response.use(
  function (res) {      
    return res;
  },
  function (error) {    
    return Promise.reject(error);
  }
);
