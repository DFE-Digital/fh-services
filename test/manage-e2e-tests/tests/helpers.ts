export const getRandomEmail = () => {
    let text = "";
    let possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (let i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text + "@" + text + '.com';
}


const generateRandomText = () => {
    let text = "";
    let possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (let i = 0; i < 6; i++) // Generates a 6-character string as per the function's logic
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
};

export const getRandomFullName = () => {
    const firstName = generateRandomText();
    const surname = generateRandomText();
    return `${firstName} ${surname}`;
};

const getFormattedDate = () => {
    return new Date().toLocaleDateString('en-GB', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
    });
};

export const getRandomServiceName = () => {
    const text = generateRandomText();
    const date = getFormattedDate();
    return `Automated Test LA Service ${text} ${date}`;
};


export const getRandomVCFServiceName = () => {
    const text = generateRandomText();
    const date = getFormattedDate();
    return `Automated Test VCS Service ${text} ${date}`;
};
