const API_URL = 'http://localhost:52144/api/events';


export const createEvent = async (data) => {
    let api = `${API_URL}/create`;
     let options = {
        method: 'post',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            authorId: 'd8dd385f-b87d-4136-da0e-08d73b6c836b',
            price: data.eventPrice,
            name: data.eventName,
            content: data.eventDescription,
            image: data.file,
            category: data.eventCategory,
            streetName: data.eventStreet,
            cityName: data.eventCity,
        })
    }
    console.log(options);

    console.log(data);
    return await fetch(api, options)
        .then(resp => {
            console.log(resp)
            if (resp.status != 200)
                throw "Stworzenie wydarzenia się nie powiodło"
            return resp.json();
        })
        .catch(e => console.log(e));
}
