import { getToken } from '../utils/JwtUtils';

const API_URL = 'http://localhost:50882/api/comments';

export const sendComment = (eventId, content, image) => {
    const formData = new FormData();
    formData.append('eventId', eventId);
    formData.append('content', content);
    formData.append('image', image);
    let api = `${API_URL}/create`;
    let options = {
        method: 'post',
        headers: {
            'Authorization': `Bearer ${getToken()}`
        },
        body: formData
    };
    
    return fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie komentarza podczas tworzenia się nie powiodło"
            return resp.json();
        })
        .then(comments => {
            return comments;
        })
        .catch(e => console.log(e));
}

export const getMoreComments = (eventId, page) => {
    let api = `${API_URL}/get?eventId=${eventId}&page=${page}`;
    let options = {
        method: 'get',
        headers: {
            'content-type': 'application/json',
            'Authorization': `Bearer ${getToken()}`
        }
    }
    return fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Pobranie komentarzy się nie powiodło"
            return resp.json();
        })
        .then(comments => {
            return comments
        })
        .catch(e => console.log(e));
}

export const deleteComment = (commentId) => {
    let api = `${API_URL}/${commentId}`;
    let options = {
        method: 'delete',
        headers: {
            'Authorization': `Bearer ${getToken()}`
        }
    }
    return fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Usunięcie komentarza się nie powiodło";
        })
        .catch(e => console.log(e));
}


export const editComment = (commentId, content) => {
    let api = `${API_URL}/edit`;
    let options = {
        method: 'put',
        headers: {
            'Authorization': `Bearer ${getToken()}`,
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            content: content,
            commentId: commentId
        })
    };
    return fetch(api, options)
        .then(resp => {
            if (resp.status !== 200)
                throw "Edycja komentarza się nie powiodła";
        })
        .catch(e => console.log(e));
}