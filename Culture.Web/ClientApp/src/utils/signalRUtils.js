import { getToken } from '../utils/JwtUtils';
import * as signalR from '@aspnet/signalr';

var connection;

export const createConnection =  () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl(`/notification?access_token=${getToken()}`)
        .build();

    connection.serverTimeoutInMilliseconds = 30000;
    connection.keepAliveIntervalInMilliseconds = 10000;
    connection.start();
};

export const getConnection = () => {

    return connection;
};