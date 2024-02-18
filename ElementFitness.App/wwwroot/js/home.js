const calculateBMI = () => {
    $("#fill-msg").attr("hidden", true);
    const height = $('#bmi-calculator-height').val();
    const weight = $("#bmi-calculator-weight").val();
    const age = $("#bmi-calculator-age").val();
    const gender = $("#bmi-calculator-gender option:selected").text();
    

    if (height === '' || weight === "" || age === "" || gender.toLowerCase() === "gender") {
        $("#fill-msg").attr("hidden", false);
        return;
    }
       
    const BMI = weight / ((height * 0.01) * (height * 0.01))

    if (BMI < 18.5) {
        $("#bmi-result").attr('hidden',false);
        $("#bmi-result-msg").text("Looks like you are underweight!");
        $("#bmi-result-msg").attr('style', 'color: yellow');
    } else if (BMI >= 18.5 && BMI < 24.9) {
        $("#bmi-result").attr("hidden", false);
        $("#bmi-result-msg").text("Congrats, You are healthy!");
        $("#bmi-result-msg").attr("style", "color: #77dd77");
    } else if (BMI >= 25 && BMI < 29.9) {
        $("#bmi-result").attr("hidden", false);
        $("#bmi-result-msg").text("Looks like you are overweight!");
        $("#bmi-result-msg").attr("style", "color: yellow");
    } else {
        $("#bmi-result").attr("hidden", false);
        $("#bmi-result-msg").text("Looks like you are Obese!");
        $("#bmi-result-msg").attr("style", "color: red");
    }
    $("#bmi-result-msg-2").text(`Your BMI is ${BMI.toFixed(1)}`);
    
    
}

const removeResult = () => {
    $("#bmi-result").attr('hidden',true);
} 


$("#bmi-calculator-height").on('keyup', () => {$("#fill-msg").attr("hidden", true);});
$("#bmi-calculator-weight").on("keyup", () => {
  $("#fill-msg").attr("hidden", true);
});
$("#bmi-calculator-age").on("keyup", () => {
  $("#fill-msg").attr("hidden", true);
});
$("#bmi-calculator-gender").on("change", () => {
  $("#fill-msg").attr("hidden", true);
});

