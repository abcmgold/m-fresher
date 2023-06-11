/**
 * Format tiền
 * Author: BATUAN (28/05/2023) 
 */
export function formatMoney(money) {
    money = parseFloat(money).toFixed(0);
    money = money.toString();
    return money.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
}

/**
 * Format ngày tháng
 * Author: BATUAN (29/05/2023) 
 */
export function formatCurrentDate() {
    const currentDate = new Date();
    const day = currentDate.getDate();
    const formattedDay = day.toLocaleString('en-US', {
        minimumIntegerDigits: 2,
        useGrouping: false
    });

    const month = currentDate.getMonth() + 1;
    const formattedMonth = month.toLocaleString('en-US', {
        minimumIntegerDigits: 2,
        useGrouping: false
    });
    const year = currentDate.getFullYear();

    return `${year}-${formattedMonth}-${formattedDay}`
}
