let mainCalText = document.getElementById("standardMainText");
let mainCalResult = document.getElementById('standardMainResult');

let mainCalNum1 = document.getElementById("standardNum1");
let mainCalNum2 = document.getElementById("standardNum2");
let mainCalNum3 = document.getElementById("standardNum3");
let mainCalNum4 = document.getElementById("standardNum4");
let mainCalNum5 = document.getElementById("standardNum5");
let mainCalNum6 = document.getElementById("standardNum6");
let mainCalNum7 = document.getElementById("standardNum7");
let mainCalNum8 = document.getElementById("standardNum8");
let mainCalNum9 = document.getElementById("standardNum9");
let mainCalNum0 = document.getElementById("standardNum0");
let mainCalDot = document.getElementById("standardDot");
let mainCalMul = document.getElementById("standardMultiply");
let mainCalDiv = document.getElementById("standardDivide");
let mainCalMin = document.getElementById("standardMinus");
let mainCalPlus = document.getElementById("standardPlus");
let mainCalEqual = document.getElementById("standardEqual");
let mainCalDel = document.getElementById("standardDel");
let mainCalC = document.getElementById("standardC");
let mainCalMod = document.getElementById("standardMod");

let mainCalRandomNumber = document.getElementById("standardRandomNumber");
let mainCalTotalNum = document.getElementById("standardTotalNumbers");
let mainCalNumBefore = document.getElementById("standardTotalNumbersBefore");
let mainCalNumAfter = document.getElementById("standardTotalNumbersAfter");

const calculator = {
  displayValue: '',
  firstOperand: null,
  waitingForSecondOperand: false,
  operator: null,
};




//main code start
mainCalText.onkeypress = (e) => checkingInput(mainCalText.value, e);



mainCalNum1.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '1'))

mainCalNum2.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '2'))

mainCalNum3.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '3'))

mainCalNum4.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '4'))

mainCalNum5.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '5'))

mainCalNum6.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '6'))

mainCalNum7.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '7'))

mainCalNum8.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '8'))

mainCalNum9.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '9'))

mainCalNum0.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '0'))

mainCalDot.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '.'))

mainCalMul.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '*'))

mainCalDiv.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '/'))

mainCalMin.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '-'))

mainCalPlus.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '+'))

mainCalEqual.addEventListener('click', () => evaluation())

mainCalMod.addEventListener("click", () => StandardInsertingNumbers(mainCalText.value, '%'))
mainCalDel.addEventListener("click", () => back())
mainCalC.addEventListener("click", () => standardCalC())

mainCalRandomNumber.addEventListener('click', () => randomNumber())

mainCalTotalNum.addEventListener("click", () => standardTotalNumbers())

mainCalNumBefore.addEventListener("click", () => standardBeforedotDigits())

mainCalNumAfter.addEventListener("click", () => standardAfterdotDigits())



//main code end

//Functions


function checkingInput(mainText, e) {
  console.log(e);
  if (e.charCode == 13) {
    evaluation();
  }

  if (e.key == '%') {
    mainCalText.value = mainText + "%";
  }
  if (e.key == '*') {
    mainCalText.value = mainText + "*";
  }
  if (e.key == '+') {
    mainCalText.value = mainText + "+";
  }
  if (e.key == '-') {
    mainCalText.value = mainText + "-";
  }
  if (e.key == '/') {
    mainCalText.value = mainText + "/";
  }
  var charCode = (e.which) ? e.which : e.keyCode
  if (charCode > 31 && (charCode != 46 && (charCode < 48 || charCode > 57))) {

    return false;
  }
  else {
    if ((e.keyCode === 46 || e == '.') && mainText.split('.').length === 2) {
      return false;
    }
    else {
      return true;
    }
  }
}
  function StandardInsertingNumbers(mainText, num) {
    if (checkingInput(mainText, num)) {
      mainCalText.value = mainText + num;
      mainCalText.focus();
    }


  }
  function evaluation() {
    try {
      mainCalResult.value = eval(mainCalText.value);
    }
    catch (error) {
      alert('Error in calculation, Please check your input!');
    }
  }
  function back() {

    mainCalText.value = mainCalText.value.substr(0, mainCalText.value.length - 1);
    mainCalText.focus();
  }
  function standardCalC() {
    mainCalResult.value = '';
    mainCalText.value = '';
  }

  function randomNumber() {
    mainCalText.value = Math.floor(Math.random() * 10000);
  }
  function standardTotalNumbers() {
    if (mainCalText.value != '') {
      try {
        mainCalResult.value = eval(mainCalText.value);
      }
      catch (error) {
        alert('Error, please check your input');
        return;
      }
      if( mainCalResult.value > Math.floor(mainCalResult.value)){

        alert('The total number of digits is: ' + (mainCalResult.value.length-1));
      }
      else{

        alert('The total number of digits is: ' + mainCalResult.value.length);
      }
    }
    else {
      alert("The total number of digits is: 0")
    }
  }

  function standardAfterdotDigits() {
    if (mainCalText.value.includes('.')) {
      console.log(". detected")
      try {
        mainCalResult.value = eval(mainCalText.value);
        alert('The total number of digits after the decimal point is: ' + mainCalResult.value.split('.')[1].length);
      } catch (error) {
        alert('please check your input');
      }
    }
    else {
      alert('There is no decimal point')
    }
  }

  function standardBeforedotDigits() {
    if (mainCalText.value.includes('.')) {
      console.log(". detected")
      try {
        mainCalResult.value = eval(mainCalText.value);
        alert('The total number of digits before the decimal point is: ' + mainCalResult.value.split('.')[0].length);
      } catch (error) {
        alert('please check your input');
      }
    }
    else {
      alert('There is no decimal point')
    }
  }
//.................




