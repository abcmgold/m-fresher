const ENUM = {
    maxLengthNumber: 18,
    maxLengthCode: 20,
    maxLengthText: 255,
    maxLengthName: 100,
    
    formAdd: 1,
    formUpdate: 2,
    formDuplicate: 3,
    delete: 4,

    statusCode: {
        created: 201,
        success: 200,
        badRequest: 400,
        conflic: 409,
        serverError: 500
    },

    statusRecord: {
        insert : 2,
        update : 1,
        noChange : 0,
        delete : -1
    }
};

export default ENUM;
