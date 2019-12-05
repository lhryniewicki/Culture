import { getToken } from '../utils/JwtUtils';

const API_URL = 'http://localhost:50882/api/account';

export const signIn = async (userName, password) => {
    const api = `${API_URL}/login`;
    const options = {
        method: 'post',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            userName: userName,
            password: password
        })
    };
    return await fetch(api, options)
        .then(resp => {
            console.log(resp.status)
            if (resp.status !== 200) {
                throw resp;
            }

            return resp.json();
        })
        .then(token => {

            return token;
        })
};

export const getEventTokenApi = async () => {
    const api = `${API_URL}/unlogged`;
    const options = {
        method: 'post',
        headers: {
            'content-type': 'application/json'
        }
    };
    return await fetch(api, options)
        .then(resp => {
            console.log(resp.status)
            if (resp.status !== 200) {
                throw resp;
            }

            return resp.json();
        })
        .then(token => {

            return token;
        })
};


export const register = async (data) => {
    const api = `${API_URL}/register`;
    const options = {
        method: 'post',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            firstName: data.firstName,
            lastName: data.lastName,
            userName: data.userName,
            password: data.password,
            email: data.email,
            secretQuestion: data.question,
            secretAnswer: data.answer
        })
    };

    await fetch(api, options)   
        .then(resp => {
            if (resp.status !== 200) {
                        throw resp;
                    }
            return resp.json()
        })  
        .then(resp => {
            console.log("login")
            localStorage.setItem('token', resp);      
            localStorage.setItem('eventToken', resp);                

        })
};

export const getUserData = async (userId) => {
    const api = `${API_URL}/user?userId=${userId}`;
    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json',
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie danych uzytkownika się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const updateUserData = async (data) => {
    const api = `${API_URL}/user`;

    const formData = new FormData();

    formData.append('username', data.username);
    formData.append('firstName', data.firstName);
    formData.append('lastName', data.lastName);
    formData.append('email', data.email);
    formData.append('image', data.file);

    const options = {
        method: 'put',
        body: formData,
        headers: {
            'Authorization': `Bearer ${getToken()}`
        }
    };

    return await fetch(api, options)
        .then(resp => {
            console.log(resp)
            if (resp.status !== 200) {
                throw resp;
            }
        });
}

export const updateUserConfig = async (data) => {
    const api = `${API_URL}/user/config`;
    console.log(data);
    const options = {
        method: 'put',
        body: JSON.stringify({
        commentsDisplayAmount: data.commentsDisplayAmount,
        eventsDisplayAmount: data.eventsDisplayAmount,
        logOutAfter: data.logOutAfter,
        anonymous: data.anonymous,
        sendEmailNotification: data.sendEmailNotification,
        calendarPastEvents: data.calendarPastEvents,
        }),
        headers: {
            'Authorization': `Bearer ${getToken()}`,
            'content-type': 'application/json'

        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie danych uzytkownika się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
}


export const getSecretQuestion = async (userName) => {
    const api = `${API_URL}/user/question?username=${userName}`;

    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200) {
                throw resp;
            }
            else {
                return resp.json();
            }
        })
}

export const checkAnswer = async (answer, userName) => {
    const api = `${API_URL}/user/answer?username=${userName}&answer=${answer}`;

    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Sprawdzenie pytania użytkownika się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const sendPassword = async (password, userName) => {
    const api = `${API_URL}/user/password?username=${userName}&password=${password}`;

    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    };

    return await fetch(api, options)
        .then(resp => {
            console.log(resp)
            if (resp.status !== 200)
                throw "Zmiana hasła użytkownika się nie powiodła";
        })
            }