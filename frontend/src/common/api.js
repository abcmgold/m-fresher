import instance from "./instance";

export const getRecord = async(url) => {
    let res = await instance.get(url)
    return res;
}

export const updateRecord = async(url, record) => {
    let res = await instance.put(url, record)
    return res;
}

export const deleteRecord = async(url) => {
    let res = await instance.delete(url)
    return res;
}

export const insertRecord = async(url, record) =>  {
    let res = await instance.post(url,record)
    return res;
}

import * as request from '@/common/api';

export default request;
