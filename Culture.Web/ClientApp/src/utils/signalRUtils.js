import { getToken, getEventToken } from '../utils/JwtUtils';
import * as signalR from '@aspnet/signalr';

var connection;

var eventConnection;
export const createConnection =  () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl(`/notification?access_token=${getToken()}`)
        .build();

    connection.serverTimeoutInMilliseconds = 30000;
    connection.keepAliveIntervalInMilliseconds = 10000;
   connection.start().then(function () {
    console.log("connected");
});
};

export const createEventConnection =  () => {
    eventConnection = new signalR.HubConnectionBuilder()
        .withUrl(`/event?access_token=${getEventToken()}`)
        .build();
        
    eventConnection.serverTimeoutInMilliseconds = 30000;
    eventConnection.keepAliveIntervalInMilliseconds = 10000;
    return eventConnection.start().then(
        function () {
            console.log("connected bhehe")
            return eventConnection;
        }
    );
};



export const getConnection = () => {

    return connection;
};

export const getEventConnection = () => {

    return eventConnection;
};

export const disconnectEventConnection = () => {

    eventConnection.stop();
    eventConnection = (function () { return; })();
};
