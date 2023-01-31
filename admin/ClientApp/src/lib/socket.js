import {io} from "socket.io-client";
import config from '../config/config.json'

export let socket = null;

export const connect = (username) => {
  socket = io(config.SOCKET_URL, {
    reconnectionDelayMax: 10000,
    extraHeaders: {
      channel: config.SOCKET_USERNAME + '_' + username,
      username: config.SOCKET_USERNAME,
      password: config.SOCKET_PASSWORD,
    },
  });
  socket.connect()
};

export const disconnect = () => {
  if(socket){
    socket.disconnect();
    socket = null;
  }
};
