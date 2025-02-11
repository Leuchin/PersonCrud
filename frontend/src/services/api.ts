import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5108/api/persons',
});

export default api;