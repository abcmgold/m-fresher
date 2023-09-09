import ENUM from "./enum";
function exception(statusCode, message, documentInfo, showDialog) {
    switch (statusCode) {
        case ENUM.statusCode.serverError:
            showDialog(message, documentInfo);
            break;
        default:
            showDialog(message.join(', '), documentInfo);
            break;
    }
}
export default exception;