// Variable Declaration
var ProgrammerResult = document.getElementById('ProgrammerMainResult')
var ProgrammerText = document.getElementById('ProgrammerMainText')

let programmerNum1 = document.getElementById('ProgrammerNum1')
let programmerNum2 = document.getElementById('ProgrammerNum2')
let programmerNum3 = document.getElementById('ProgrammerNum3')
let programmerNum4 = document.getElementById('ProgrammerNum4')
let programmerNum5 = document.getElementById('ProgrammerNum5')
let programmerNum6 = document.getElementById('ProgrammerNum6')
let programmerNum7 = document.getElementById('ProgrammerNum7')
let programmerNum8 = document.getElementById('ProgrammerNum8')
let programmerNum9 = document.getElementById('ProgrammerNum9')
let programmerNum0 = document.getElementById('ProgrammerNum0')

let programmerdot = document.getElementById('ProgrammerDot')

let programmerA = document.getElementById('ProgrammerA')
let programmerB = document.getElementById('ProgrammerB')
let programmerC = document.getElementById('ProgrammerC')
let programmerD = document.getElementById('ProgrammerD')
let programmerE = document.getElementById('ProgrammerE')
let programmerF = document.getElementById('ProgrammerF')


let programmerDel = document.getElementById('ProgrammerDel')
let programmerCreset = document.getElementById('ProgrammerCreset')

let programmerBinToDecimal = document.getElementById('ProgrammerBinaryToDecimal')
let programmerDecimalToBin = document.getElementById('ProgrammerDecimalToBinary')
let programmerHexToDecimal = document.getElementById('ProgrammerHexToDecimal')
let programmerDecimalToHex = document.getElementById('ProgrammerDecimalToHex')



// Variable Declarion End
//main Code
ProgrammerText.onkeypress = (e) => checkingInput(ProgrammerText.value, e);
programmerA.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, 'a'))
programmerB.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, 'b'))
programmerC.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, 'c'))
programmerD.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, 'd'))
programmerE.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, 'e'));
programmerF.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, 'f'))

programmerNum0.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '0'))
programmerNum1.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '1'))
programmerNum2.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '2'))
programmerNum3.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '3'))
programmerNum4.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '4'))
programmerNum5.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '5'))
programmerNum6.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '6'))
programmerNum7.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '7'))
programmerNum8.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '8'))
programmerNum9.addEventListener('click', () => ProgrammerInsertingNumbers(ProgrammerText.value, '9'))
programmerdot.addEventListener('click',() => ProgrammerInsertingNumbers(ProgrammerText.value, '.'))

programmerCreset.addEventListener('click', () => ProgrammerErase())
programmerDel.addEventListener('click', () => back())

programmerBinToDecimal.addEventListener('click', () => BinToDec())
programmerDecimalToBin.addEventListener('click', () => DecToBin())
programmerHexToDecimal.addEventListener('click', () => HexToDec())
programmerDecimalToHex.addEventListener('click', () => DecToHex())
//Main Code end
//Functions
function checkingInput(mainText, e) {
    console.log(e);
    if (e.key == 'A' || e.key == 'B' || e.key == 'C' || e.key == 'D' || e.key == 'E' || e.key == 'F') {
        alert('Please write in lowercase not capital')
    }
    if (e.key == 'a') {
        ProgrammerText.value = mainText + 'a'
    }
    if (e.key == 'b') {
        ProgrammerText.value = mainText + 'b'
    }
    if (e.key == 'c') {
        ProgrammerText.value = mainText + 'c'
    }
    if (e.key == 'd') {
        ProgrammerText.value = mainText + 'd'
    }
    if (e.key == 'e') {
        ProgrammerText.value = mainText + 'e'
    }
    if (e.key == 'f') {
        ProgrammerText.value = mainText + 'f'
    }
    var charCode = (e.which) ? e.which : e.keyCode
    if (charCode > 31 && (charCode != 46 && (charCode < 48 || charCode > 57))) {

        return false;
    } else {
        if ((e.keyCode === 46 || e == '.') && mainText.split('.').length === 2) {
            return false;
        }
        else {
            return true;
        }
    }
}

    function ProgrammerInsertingNumbers(mainText, num) {
        if (checkingInput(mainText, num)) {
            ProgrammerText.value = mainText + num;
            ProgrammerText.focus();
        }


    }

    function back() {

        ProgrammerText.value = ProgrammerText.value.substr(0, ProgrammerText.value.length - 1);
        ProgrammerText.focus();
    }

    function ProgrammerErase() {
        ProgrammerText.value = '';
        ProgrammerResult.value = '';
    }

    function BinToDec() {
        if (ProgrammerText.value == '') {
            alert('Please enter a binary number');
            return;
        }
        try {
            eval(ProgrammerText.value)
        } catch (error) {
            alert('Please enter a binary number')
            return;
        }
        for (var i = 0; i < ProgrammerText.value.length; i++) {
            var number = parseInt(ProgrammerText.value[i]);
            if (number >= 2) {
                alert("Please enter a binary number.");
                return;

            }
        }
        ProgrammerResult.value = parseInt(ProgrammerText.value, 2);
    }

    function DecToBin() {
        if (ProgrammerText.value == '') {
            alert('Please enter a Decimal number');
            return;
        }
        try {
            eval(ProgrammerText.value)
        } catch (error) {
            alert('Please enter a decimal number')
            return;
        }
        ProgrammerResult.value = eval(ProgrammerText.value).toString(2);
    }

    function HexToDec() {
        if (ProgrammerText.value == '') {
            alert('Please enter a Hex number');
            return;
        }
        try {
            ProgrammerResult.value = parseInt(ProgrammerText.value, 16);
        } catch (error) {
            alert('Please enter a hex number')
            return;
        }

    }

    function DecToHex() {
        if (ProgrammerText.value == '') {
            alert('Please enter a Decimal number');
            return;
        }
        try {
            eval(ProgrammerText.value)
        } catch (error) {
            alert('Please enter a decimal number')
            return;
        }
        ProgrammerResult.value = eval(ProgrammerText.value).toString(16);
    }
//Functions End