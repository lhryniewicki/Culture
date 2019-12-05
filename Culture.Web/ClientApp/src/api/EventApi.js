import { getToken, getEventToken } from '../utils/JwtUtils';

const API_URL = 'http://localhost:50882/api/events';


export const createEvent = async (data) => {
    const formData = new FormData();

    data.date.forEach(element => formData.append('eventDate', element));

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
        headers: {
            'Authorization': `Bearer ${getToken()}`
        },
        body: formData
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw resp;

            return "success";
        });
}


export const getPreviewEventList = async (page, category, query) => {
    console.log(category);
    let categoryApi;
    let queryApi;
    category === null || category === "Wszystkie" ? categoryApi = "" : categoryApi = `&category=${category}`;
    query === "" ? queryApi = "" : queryApi = `&query=${query}`;
    console.log(queryApi);
    let api = `${API_URL}/get/preview?page=${page}${categoryApi}${queryApi}`;
    let options = {
        method: 'get',
        headers: {
            'content-type': 'application/json',
            'Authorization': `Bearer ${getEventToken()}`
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie wydarzen się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
};
export const sendReaction = async (eventId, reactionType) => {

    let api = `${API_URL}/reaction`;
    let options = {
        method: 'post',
        headers: {
            'content-type': 'application/json',
            'Authorization': `Bearer ${getToken()}`
        },
        body: JSON.stringify({
            eventId: eventId,
            reactionType: reactionType
        })

    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Wyslanie reakcji się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
};

export const getEventDetails = async (eventId) => {
    let api = `${API_URL}/get/details/${eventId}`;
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
                throw "Pobieranie szczegolow wydarzenia się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
};



export const getMap = async () => {
    const api = `${API_URL}/geolocation`;
    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json',
            'Authorization': `Bearer ${getToken()}`
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie kordow do mapy się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
};


export const getParticipants = async (eventId,query) => {
    let api = `${API_URL}/get/participants/${eventId}`;

    if (query !== null && query !== undefined) api = api + `?query=${query}`;

    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie uczestników imprezy się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
};

export const getEventMap = async (eventId) => {
    let api = `${API_URL}/get/map/${eventId}`;


    const options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie punktu imprezy na mapie się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
};

export const getAllMap = async (query,dates,category) => {
    console.log("hehehehehehe");
    let api = `${API_URL}/get/allMap`;
    const options = {
        method: 'post',
        body: JSON.stringify({
            query: query,
            dates:  dates  ,
            category: category
        }),
        headers: {
            'content-type': 'application/json'
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie mapy się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
};

export const getAllCalendar = async (query, dates, category) => {
    let api = `${API_URL}/get/allCalendar`;
    const options = {
        method: 'post',
        body: JSON.stringify({
            query: query,
            dates: dates,
            category: category
        }),
        headers: {
            'content-type': 'application/json'
        }
    };

    return await fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie kalendarza się nie powiodło";
            return resp.json();
        })
        .catch(e => console.log(e));
};


export const editEvent = (id,
   name,
    category,
   takesPlaceDate,
    takesPlaceHour,
   price,
    cityName,
    content,
    streetName) => {
    let api = `${API_URL}/edit`;
    let options = {
        method: 'put',
        headers: {
            'Authorization': `Bearer ${getToken()}`,
            'content-type': 'application/json'
        },
        body: JSON.stringify({
           id: id,
            name: name,
            category: category,
            EventDate: takesPlaceDate.split('-').reverse(),
            EventTime: takesPlaceHour,
            price: price,
            cityName: cityName,
            content:content,
            streetName: streetName
        })

    };
    return fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Edycja wydarzenia się nie powiodła";
        })
        .catch(e => console.log(e));
}


export const deleteEvent = (id) => {
    let api = `${API_URL}/delete/${id}`;
    let options = {
        method: 'delete',
        headers: {
            'Authorization': `Bearer ${getToken()}`,
            'content-type': 'application/json'
        }

    };
    return fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Edycja wydarzenia się nie powiodła";
        })
        .catch(e => console.log(e));
}

