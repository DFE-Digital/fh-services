export const getRandomEmail = () => {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for( var i=0; i < 5; i++ )
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text + "@" + text + '.com';
}

export const getRandomFullName = () => {
    var firstName = "";
    var surname = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for( var i=0; i < 5; i++ )
        firstName += possible.charAt(Math.floor(Math.random() * possible.length));

    for( var i=0; i < 5; i++ )
        surname += possible.charAt(Math.floor(Math.random() * possible.length));

    return firstName + " " + surname;
}