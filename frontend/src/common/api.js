import instance from "./instance";

export const getRecord = async(url, body) => {
    let res = await instance.get(url, body)
    return res;
}

export const updateRecord = async(url, record) => {
    let res = await instance.put(url, record)
    return res;
}

export const deleteRecord = async(url, listId) => {
    let res = await instance.delete(url, listId)
    return res;
}


export const insertRecord = async(url, body) =>  {
    let res = await instance.post(url,body)
    return res;
}

import * as request from '@/common/api';

export default request;
