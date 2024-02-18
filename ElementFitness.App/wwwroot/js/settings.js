$(document).ready(() => {
  let msg = $("#error-message").val();
  if (msg.length > 0) {
    msg = msg.replace(/\\n/g, "\n");
    alert(msg);
    $("#error-message").attr("value", "");
  }
    
    $("#field-match-msg").attr("hidden", true);
});



const checkInput = () => {
    const newPasswordInput = $("#NewPasswordInput").val().trim();
    const confirmPasswordInput = $("#ConfirmPasswordInput").val().trim();
    const currentPasswordInput = $("#CurrentPasswordInput").val().trim();
    
    if (
      currentPasswordInput !== "" &&
      newPasswordInput!=="" && confirmPasswordInput!=="" && (newPasswordInput === confirmPasswordInput)
    ) {
      $("#disabled-change-btn").attr("hidden", true);
      $("#change-btn").attr("hidden", false);
      $("#field-match-msg").attr("hidden", true);
    } else {
      $("#disabled-change-btn").attr("hidden", false);
      $("#change-btn").attr("hidden", true);
      $("#field-match-msg").attr("hidden", false);
    }
    
    if (newPasswordInput === "" && confirmPasswordInput === "")
    {
        $("#field-match-msg").attr("hidden", true);
    }
};


$(document).on("keyup", "#NewPasswordInput", checkInput);
$(document).on("keyup", "#ConfirmPasswordInput", checkInput);
$(document).on("keyup", "#CurrentPasswordInput", checkInput);
;