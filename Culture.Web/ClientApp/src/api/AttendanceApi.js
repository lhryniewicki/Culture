const API_URL = 'http://localhost:52144/api/attendance';


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