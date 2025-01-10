export const getRandomEmail = () => {
    let text = "";
    let possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (let i = 0; i < 5; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text + "@" + text + '.com';
}

export const getRandomFullName = () => {
    let firstName = "";
    let surname = "";
    let possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (let i = 0; i < 5; i++)
        firstName += possible.charAt(Math.floor(Math.random() * possible.length));

    for (let i = 0; i < 5; i++)
        surname += possible.charAt(Math.floor(Math.random() * possible.length));

    return firstName + " " + surname;
}