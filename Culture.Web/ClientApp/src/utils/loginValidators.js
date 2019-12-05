export const validatePasswordInput = (input1, input2) => {
    let errors = [];
    
    if (input1.length < 6) errors.push('Hasło powinno mieć przynajmniej 6 znaków.');

    if (!hasUpperCase(input1)) errors.push('Hasło powinno zawierać przynajmniej jedną wielką literę.');

    if (input1 !== input2) errors.push('Hasła się różnią.');

    if (!hasSpecialChar(input1)) errors.push('Hasło powinno zawierać przynajmniej jeden znak specjalny.');

    if (!hasDigit(input1)) errors.push('Hasło powinno zawierać przynajmniej jedną cyfrę')
    console.log(errors);
    return errors;
}

export const validateEmail = (email) => {
    let errors = [];

    if (!isEmail(email)) errors.push('Format emaila się nie zgadza.');

    return errors;
}

function isEmail(email) {
    return (/\S+@\S+\.\S+/.test(email));
}

function hasUpperCase(str) {
    return (/[A-Z]/.test(str));
}

function hasDigit(str) {
    return (/[A-Z]/.test(str));
}

function hasSpecialChar(str) {
    return (/[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(str));
}