import axios from 'axios'
const instance = axios.create({baseURL: 'https://localhost:7005/api/v1/', timeout: 1000});

instance.interceptors.response.use((response) => {
    return response
}, (error) => {
    console.log(error)
    if (!error.response) {
        return Promise.reject("Server sập")
    } else {
        if (error.response.data.Errors) {
            let listError = "";
            error.response.data.Errors.forEach((err) => {
                listError += err.ErrorMessage;
            })
            return Promise.reject(listError);
        } else {
            return Promise.reject(error.response.data.UserMessage);
        }
    }

})

export default instance;
