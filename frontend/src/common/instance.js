import axios from 'axios'
const instance = axios.create({
    baseURL: 'https://localhost:7005/api/v1/',
    timeout: 1000,
});

export default instance;
