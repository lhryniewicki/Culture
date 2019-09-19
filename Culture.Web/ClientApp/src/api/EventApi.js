const API_URL = 'http://localhost:52144/api/events';


export const createEvent = async (data) => {
    let formData = new FormData();

    for (var i = 0; i < data.date.length; i++) {
        formData.append('eventDate', data.date[i]);
    }

    formData.append('authorId', 'd8dd385f-b87d-4136-da0e-08d73b6c836b');
    formData.append('price', data.eventPrice);
    formData.append('name', data.eventName);
    formData.append('content', data.eventDescription);
    formData.append('image', data.file);
    formData.append('category', data.eventCategory);
    formData.append('streetName', data.eventStreet);
    formData.append('cityName', data.eventCity);
    formData.append('eventTime', data.time);
    let api = `${API_URL}/create`;
     let options = {
        method: 'post',
         body: formData
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status != 200)
                throw "Stworzenie wydarzenia się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}


export const getPreviewEventList = async (page,category) => {
    let categoryApi;
    category === null ? categoryApi = "" : categoryApi = `&category=${category}`;
    let api = `${API_URL}/get?page=${page}${categoryApi}`;
    let options = {
        method: 'get',
    }

    return await fetch(api, options)
        .then(resp => {
            if (resp.status != 200)
                throw "Pobranie wydarzen się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}