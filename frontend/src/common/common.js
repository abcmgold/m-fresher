/**
 * Format tiền
 * Author: BATUAN (28/05/2023) 
 */
export function formatMoney(money) {
    try {
        if (money) {
            money = parseFloat(money).toFixed(0);
            money = money.toString();
            return money.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
        } else {
            return 0;
        }

    } catch (error) {
        return error
    }
}

/**
 * Bỏ format tiền
 * Author: BATUAN (28/05/2023) 
 */
export function unformatMoney(money) {
    try {
        if (money) {
            var value = parseInt(money.toString().replaceAll('.', ''));
            return value;
        } else {
            return '';
        }
    } catch (error) {
        console.log(error);
    }
}

/**
 * Format ngày tháng
 * Author: BATUAN (29/05/2023) 
 */
export function formatCurrentDate(date) {
    var dateObject = new Date(date);

    const day = dateObject.getDate();
    const formattedDay = day.toLocaleString('en-US', {
        minimumIntegerDigits: 2,
        useGrouping: false
    });

    const month = dateObject.getMonth() + 1;
    const formattedMonth = month.toLocaleString('en-US', {
        minimumIntegerDigits: 2,
        useGrouping: false
    });
    const year = dateObject.getFullYear();

    return `${formattedDay}-${formattedMonth}-${year}`
}

/**
 * Format tỷ lệ
 * Author: BATUAN (29/05/2023) 
 */
export function formatRatio(number) {
    if (!number) {
        return 0;
    }
    let formatted_number = new Intl.NumberFormat('vi-VN', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    }).format(number);
    return formatted_number
}

export function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}