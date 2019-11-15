export const decodeJwt = (token) => {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace('-', '+').replace('_', '/');
    return JSON.parse(window.atob(base64));
};

export const getUserId = () => {
    const token = getToken();
    if (token !== null && token !== undefined) {
        return decodeJwt(localStorage.getItem('token')).jti;
    }
    return null;
};

export const userIsAuthenticated = () => {
    const token = getToken();
    if (token !== null && token !== undefined) {
        const decodedToken = decodeJwt(token);
        const currentUnixTimestamp = Math.round((new Date()).getTime() / 1000);

        if (decodedToken.exp > currentUnixTimestamp) {
            return true;
        }
    }
    return false;
};

export const saveUserToken = (token) => {
    localStorage.setItem('token', token);
};  

export const removeUserToken = () => {
    localStorage.removeItem('token');
};

export const getToken = () => {
    return localStorage.getItem('token');
};