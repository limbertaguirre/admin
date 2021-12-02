import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { connect, socket, disconnect } from "../lib/socket";
import { cerrarSesion } from "../redux/actions/LoginAction";
import { showMessage } from "../redux/actions/messageAction";

const useSocket = ({ routerRef }) => {
  const { userName, token } = useSelector((state) => state.load);
  const dispatch = useDispatch();

  useEffect(() => {
    if(token && typeof token === 'string' && token.length > 0){
      connect(userName);
      socket.on("unlogin", (data) => {
        const originalToken = token.replace('Bearer ','')
        if (data.token === originalToken) {
          socket.off("unlogin");
          disconnect();
          dispatch(showMessage({
            message: "Acabas de ingresar desde otro dispositivo.",
            variant: 'info'
          }))
          dispatch(cerrarSesion(routerRef.current.history));
        }
      });
    } else {
      if (socket && socket.connected) {
        socket.off("unlogin");
        disconnect();
      }
    }
    
    return () => {
      if (socket && socket.connected) {
        socket.off("unlogin");
        disconnect();
      }
    };
  }, [token]);

  return { socket };
};

export default useSocket;
