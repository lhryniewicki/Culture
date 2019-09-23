const API_URL = 'http://localhost:52144/api/events';


export const createEvent = async (data) => {
    const formData = new FormData();

    data.date.forEach(element => formData.append('eventDate', element));

    formData.append('authorId', 'd8dd385f-b87d-4136-da0e-08d73b6c836b');
    formData.append('price', data.eventPrice);
    formData.append('name', data.eventName);
    formData.append('content', data.eventDescription);
    formData.append('image', data.file);
    formData.append('category', data.eventCategory);
    formData.append('streetName', data.eventStreet);
    formData.append('cityName', data.eventCity);
    formData.append('eventTime', data.time);
    const api = `${API_URL}/create`;
    const options = {
        method: 'post',
         body: formData
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Stworzenie wydarzenia się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}


export const getPreviewEventList = async (page, category) => {
    let categoryApi;
    category === null ? categoryApi = "" : categoryApi = `&category=${category}`;
    let api = `${API_URL}/get?page=${page}${categoryApi}`;
    let options = {
        method: 'get'
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie wydarzen się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}
export const sendReaction= async(userId, eventId, reactionType)=>{
    
    let api = `${API_URL}/reaction`;
    let options = {
        method: 'post',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            userId: userId,
            eventId: eventId,
            reactionType: reactionType
        })

    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Wyslanie reakcji się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}

export const getEventDetails = async (eventId) => {
    let api = `${API_URL}/get/details/${eventId}`;
    let options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobieranie szczegolow wydarzenia się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}
