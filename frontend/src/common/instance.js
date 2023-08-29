import axios from 'axios'
import {MISAResource} from './resource';
import ENUM from './enum';
// import store from '@/store';
const instance = axios.create({baseURL: 'https://localhost:7005/api/v1/'});

instance.interceptors.response.use((response) => {
    return response
}, (error) => {
    if (!error.response) {
        return Promise.reject({message: MISAResource['vn-VI'].serverNotResponse, statusCode: ENUM.statusCode.serverError})
        // store.commit('toggleMaskElementShow')
        // console.log('Server error: ' + store.getters.getMaskElementShow)
    } else {
        if (error.response.data.Errors) {
            let listError = [];
            error.response.data.Errors.forEach((err) => {
                listError.push(err.Errors[err.Errors.length - 1].ErrorMessage);
            })
            return Promise.reject({message: listError, statusCode: error.response.status});
        } else {
            let listError = [];
            listError.push(error.response.data.UserMessage);
            console.log(error);
            return Promise.reject({message: listError, statusCode: error.response.status, errorField: error.response.data.ErrorField, documentInfo: error.response.data.DocumentInfo});
        }
    }
})

export default instance;
