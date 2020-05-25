//입력된 금액에 자리수마다 콤마(,) 생성
function CreateMoneyComma(str) {
    return str.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

//콤마가 생성된 금액에서 콤마를 제거
function DeleteMoneyComma(str) {
    return str.toString().replace(/,/g, '');
}

//인풋에서 숫자 입력시 자리수마다 콤마 생성
function InputNumberFormat(obj) {
    obj.value = CreateMoneyComma(DeleteMoneyComma(obj.value));
}