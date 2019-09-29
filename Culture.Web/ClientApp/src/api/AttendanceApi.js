const API_URL = 'http://localhost:50882/api/attendance';


export const addToCalendar  = async (eventId) => {
   
    const api = `${API_URL}/calendar`;
    let options = {
        method: 'post',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(
            eventId: eventId
        )
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Dodanie do kalendarza się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const removeFromCalendar = async (eventId) => {

    const api = `${API_URL}/calendar`;
    const options = {
        method: 'delete',
        headers: {
            'content-type':'application/json'
        },
        body: JSON.stringify(
            eventId: eventId
        )
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Usunięcie z kalendarza się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const getUserCalendarDays = async () => {

    const api = `${API_URL}/get`;
    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie dni z kalendarza się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const getEventsInDays = async (date) => {
    const dateTime = date.toISOString()
    const api = `${API_URL}/get/${dateTime}`;
    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie wydarzen z dnia się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const signUser = async (eventId) => {
    const api = `${API_URL}/user/sign`;
    const options = {
        method: 'post',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(
            eventId:eventId
        )
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Zapisanie użytkownika się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const unsignUser = async (eventId) => {
    const api = `${API_URL}/user/sign`;
    const options = {
        method: 'delete',
        headers: {
            'content-type': 'application/json'
        },
         body: JSON.stringify(
             eventId: eventId
         )
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie wydarzen z dnia się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}