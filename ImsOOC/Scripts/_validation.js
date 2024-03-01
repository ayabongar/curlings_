function setAllDivisions(obj1, obj2, thisField) {
        obj1.disabled = thisField.checked;
        obj2.disabled = thisField.checked;
}

function CountChars(field, maxlimit) 
{
    if (field.value.length > maxlimit)
     {
        field.value = field.value.substring(0, maxlimit);
    }
}


var upper = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ,.\/\"\'`!@#$%^&*()-+_|<>: ';
var number = '0123456789.';
var lower = 'abcdefghijklmnopqrstuvwxyz,.\/\"\'`!@#$%^&*()-+_|<>: ';
var money = '0123456789.';


function isValid(value, option) {
    if (value) {
        if (value == "" || value == '') {
            return true;
        }
        for (var i = 0; i < value.length; i++) {
            var index = option.indexOf(value.charAt(i), 0);
            if (index == -1) {
                return false;
            }
        }
    }
    return true;
}


isLower = function(field) {
    var v = trim(field.value);
    return isValid(v, lower);
};


isAlphanum = function (field) {
    var v = trim(field.value);
    var x = isValid(v, lower + '' + upper + '' + number);
    if (x == false) {

        field.focus();
        alert('This field requires alpha numerics');
        field.style.backgroundColor = "red";
        field.value = "";
    }
    else {
        field.style.backgroundColor = "transparent";
    }
    return true;
};


isAlpha = function(field) {
    var v = trim(field.value);
    var x = isValid(v, lower + upper);
    if (x == false) {

        field.focus();
        alert('This field requires ONLY alphabets');
        field.style.backgroundColor = "red";
        field.value = "";
    } else {
        field.style.backgroundColor = "transparent";
    }
};

isUpper = function(field) {
    return isValid(field.value, upper);
};


isNumber = function(field) {
    var v = trim(field.value);
    var x = isValid(v, number);
    if (x == false) {

        field.focus();
        alert('This field requires only numbers');
        field.style.backgroundColor = "red";
        field.value = "";
    } else {
        field.style.backgroundColor = "transparent";
    }
};

isPercentage = function(field) {
    var v = trim(field.value);
    var x = isValid(v, number);
    if (x == false) {

        field.focus();
        alert('This is a percentage field (0 - 100), exclude the %age sign');
        field.style.backgroundColor = "red";
        field.value = "";
    } else {
        if (parseFloat(v) > 100 || parseFloat(v) < 0) {
            field.focus();
            alert('This is a percentage field (0 - 100), exclude the %age sign');
            field.style.backgroundColor = "red";
            field.value = "";
        } else {
            field.style.backgroundColor = "transparent";
        }
    }
};

isMoney = function(field) {
    var v = trim(field.value);
    var x = isValid(v, money);
    if (x == false) {

        field.focus();
        alert('This field requires only currency value');
        field.style.backgroundColor = "red";
        field.value = "";
    } else {
        field.style.backgroundColor = "transparent";
    }
};

function trim(str) {
    if (!str || typeof str != 'string') return null; return str.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
}

validateEmail = function(field) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;


    if (field && field.value && !filter.test(field.value)) {
        field.focus();
        alert('Email Address is not valid');
        field.style.backgroundColor = "red";
        field.value = "";
    } else {
        field.style.backgroundColor = "transparent";
    }
    return false;
};

