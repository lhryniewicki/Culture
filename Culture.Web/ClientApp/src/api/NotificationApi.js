import { getToken } from '../utils/JwtUtils';

const API_URL = 'http://localhost:50882/api/notifications';

export const getNotificationsNumber = async () => {

    let api = `${API_URL}/number`;
    let options = {
        method: 'get',
        headers: {
            'content-type': 'application/json',
            'Authorization': `Bearer ${getToken()}`
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie liczby notyfikacji się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const getNotifications = async (page) => {

    let api = `${API_URL}/get?page=${page}`;
    let options = {
        method: 'get',
        headers: {
            'content-type': 'application/json',
            'Authorization': `Bearer ${getToken()}`

        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie notyfikacji się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
}