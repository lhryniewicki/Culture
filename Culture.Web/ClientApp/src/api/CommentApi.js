const API_URL = 'http://localhost:52144/api/comments';

export const sendComment = (eventId, content) => {
    let api = `${API_URL}/create`;
    let options = {
        method: 'post',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            content: content,
            eventId: eventId
        })
    }
    return fetch(api, options)
        .then(resp => {
            if (resp.status != 200)
                throw "Pobranie komentarza podczas tworzenia się nie powiodło"
            return resp.json();
        })
        .then(comments => {
            return comments
        })
        .catch(e => console.log(e));
}
export const getMoreComments = (eventId, page) => {
    console.log(eventId);
    let api = `${API_URL}/get?eventId=${eventId}&page=${page}`;
    let options = {
        method: 'get',
        headers: {
            'content-type': 'application/json'
        }
    }
    return fetch(api, options)
        .then(resp => {
            if (resp.status != 200)
                throw "Pobranie komentarzy podczas loadMore się nie powiodło"
            return resp.json();
        })
        .then(comments => {
            return comments
        })
        .catch(e => console.log(e));
}