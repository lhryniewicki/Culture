const API_URL = 'http://localhost:52144/api/account';

export const signIn = async (userName, password) => {
    let api = `${API_URL}/login`;
    let options = {
        method: 'post',
        headers:{
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            userName: userName,
            password: password
        })
    }
    return await fetch(api, options)
        .then(resp =>{ 
            if(resp.status!=200)
            throw "Logowanie się nie powiodło"
            return resp.json();
        })
        .then(token => {
            console.log(token);
            return token
        })
        .catch(e => console.log(e));
}

const register = async (data) => {
    let api = `${API_URL}/register`;
    let options = {
        method: 'post',
        headers:{
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            firstName: data.firstName,
            lastName: data.lastName,
            userName: data.userName,
            password: data.password,
            email: data.email
        })
    }

    await fetch(api, options)
        .then(resp => resp.json())
        .then(resp => {
            localStorage.setItem('token', resp);
        })
        .catch(e => console.log(e));
}

