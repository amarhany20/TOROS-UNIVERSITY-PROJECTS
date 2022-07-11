// Variable Declaration
var ScientificResult = document.getElementById('ScientificMainResult')
var ScientificText = document.getElementById('ScientificMainText')

let ScientificNum1 = document.getElementById('ScientificNum1')
let ScientificNum2 = document.getElementById('ScientificNum2')
let ScientificNum3 = document.getElementById('ScientificNum3')
let ScientificNum4 = document.getElementById('ScientificNum4')
let ScientificNum5 = document.getElementById('ScientificNum5')
let ScientificNum6 = document.getElementById('ScientificNum6')
let ScientificNum7 = document.getElementById('ScientificNum7')
let ScientificNum8 = document.getElementById('ScientificNum8')
let ScientificNum9 = document.getElementById('ScientificNum9')
let ScientificNum0 = document.getElementById('ScientificNum0')

let Scientificdot = document.getElementById('ScientificDot')

let ScientificSin = document.getElementById('ScientificSin')
let ScientificCos = document.getElementById('ScientificCos')
let ScientificTanh = document.getElementById('ScientificTanh')
let ScientificXN = document.getElementById('ScientificXN')
let ScientificLog = document.getElementById('ScientificLog')
let ScientificN = document.getElementById('ScientificN')
let ScientificExp = document.getElementById('ScientificExp')


let ScientificDel = document.getElementById('ScientificDel')
let ScientificCreset = document.getElementById('ScientificCreset')

let ScientificValidaty = document.getElementById('ScientificValidaty')
let ScientificChangeSign = document.getElementById('ScientificChangeSign')



// Variable Declarion End
//main Code
ScientificText.onkeypress = (e) => checkingInput(ScientificText.value, e);

ScientificNum0.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '0'))
ScientificNum1.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '1'))
ScientificNum2.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '2'))
ScientificNum3.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '3'))
ScientificNum4.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '4'))
ScientificNum5.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '5'))
ScientificNum6.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '6'))
ScientificNum7.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '7'))
ScientificNum8.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '8'))
ScientificNum9.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '9'))
Scientificdot.addEventListener('click', () => ScientificInsertingNumbers(ScientificText.value, '.'))

ScientificSin.addEventListener('click', () => sin())
ScientificCos.addEventListener('click', () => cos())
ScientificTanh.addEventListener('click', () => tanh())
ScientificXN.addEventListener('click', () => XN())
ScientificLog.addEventListener('click', () => log())
ScientificExp.addEventListener('click', () => exp())
ScientificN.addEventListener('click', () => factorialN(ScientificText.value))

ScientificValidaty.addEventListener('click', () => Validate())
ScientificChangeSign.addEventListener('click', () => changeSign())

ScientificCreset.addEventListener('click', () => ScientificErase())
ScientificDel.addEventListener('click', () => back())


//Main Code end
//Function Start
function checkingInput(mainText, e) {
    console.log(e);
    if (e.key == '-' && mainText.split('-').length != 2) {
        ScientificText.value = mainText + '-';
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

function ScientificInsertingNumbers(mainText, num) {
    if (checkingInput(mainText, num)) {
        ScientificText.value = mainText + num;
        ScientificText.focus();
    }


}

function back() {

    ScientificText.value = ScientificText.value.substr(0, ScientificText.value.length - 1);
    ScientificText.focus();
}

function ScientificErase() {
    ScientificText.value = '';
    ScientificResult.value = '';
}

function sin() {
    try {
        eval(ScientificText.value)
    }
    catch (error) {
        alert('Error, Please check your input')
        return;
    }
    ScientificResult.value = Math.sin(eval(ScientificText.value));
}
function cos() {
    try {
        eval(ScientificText.value)
    }
    catch (error) {
        alert('Error, Please check your input')
        return;
    }
    ScientificResult.value = Math.cos(eval(ScientificText.value));
}
function tanh() {
    try {
        eval(ScientificText.value)
    }
    catch (error) {
        alert('Error, Please check your input')
        return;
    }
    ScientificResult.value = Math.tanh(eval(ScientificText.value));
}
function log() {
    try {
        eval(ScientificText.value)
    }
    catch (error) {
        alert('Error, Please check your input')
        return;
    }
    let base = prompt("Please enter log base", "10");
    try {
        base = eval(base)
    }
    catch (error) {
        alert('Please enter the base correctly');
        return;
    }

    if (base == '10') {
        ScientificResult.value = Math.log10(eval(ScientificText.value))
    }
    else {
        ScientificResult.value = Math.log(eval(ScientificText.value)) / Math.log(base);
    }
}
function exp() {
    try {
        eval(ScientificText.value)
    }
    catch (error) {
        alert('Error, Please check your input')
        return;
    }
    ScientificResult.value = Math.exp(eval(ScientificText.value));
}
function XN() {
    try {
        eval(ScientificText.value)
    }
    catch (error) {
        alert('Error, Please check your input')
        return;
    }
        ScientificResult.value = Math.pow(eval(ScientificText.value), prompt("Please enter N", "2"));
    
}
function changeSign() {
        if (ScientificText.value.includes('-')) {
            if(ScientificText.value[0]=='-'){
                ScientificText.value = Math.abs(ScientificText.value);
            }
            else{
                alert("to change the sign from negative to positive, the negative sign has to be before the nubmer. Please check your input!")
            }
            
        }
        else {
            ScientificText.value = '-' + ScientificText.value;
        }
}
function Validate(){
    try {
        eval(ScientificText.value)
    }
    catch (error) {
        alert('Error, Your input is not valid')
        return;
    }
    alert('Your input is correct')
}
function factorialN(n){
    try {
        eval(ScientificText.value)
    }
    catch (error) {
        alert('Error, Please check your input')
        return;
    }
    let answer =1;
    if (n == 0 || n == 1){
        return ScientificResult.value= 1;
      }else{
        for(var i = n; i >= 1; i--){
          answer = answer * i;
        }
        return ScientificResult.value = answer;
      }  
    
}