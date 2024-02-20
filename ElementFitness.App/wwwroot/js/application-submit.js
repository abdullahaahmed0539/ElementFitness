$(document).ready(() => {
  $("#firstNameInput").val("");
  $("#lastNameInput").val("");
  $("#emailInput").val("");
  $("#mobileNumberInput").val("");
  $("#aboutInput").val("");
  $("#DisplayImgInput").val(null);
    
    let msg = $("#error-message").val();
    if (msg.length > 0) {
      msg = msg.replace(/\\n/g, "\n");
      alert(msg);
      $("#error-message").attr("value", "");
    }

});

$("#aboutInput").on("keyup", () => {
  const aboutInput = $("#aboutInput").val();
  $("#about-hint").text(`${aboutInput.length}/1500 characters`);
});

const checkInput = () => {
  console.log("here");
  const firstNameInput = $("#firstNameInput").val().trim();
  const lastNameInput = $("#lastNameInput").val().trim();
  const emailInput = $("#emailInput").val().trim();
  const mobileNumberInput = $("#mobileNumberInput").val().trim();
  const aboutInput = $("#aboutInput").val().trim();
  const resumeUploaded = $("#resumeUpload")[0].files.length > 0;

  if (
    firstNameInput !== "" &&
    lastNameInput !== "" &&
    emailInput !== "" &&
    mobileNumberInput !== "" &&
    aboutInput !== "" &&
    resumeUploaded
  ) {
    $("#disabled-application-submit-btn").attr("hidden", true);
    $("#application-submit-btn").attr("hidden", false);
  } else {
    $("#disabled-application-submit-btn").attr("hidden", false);
    $("#application-submit-btn").attr("hidden", true);
  }
};

$(document).on("keyup", "#firstNameInput", checkInput);
$(document).on("keyup", "#lastNameInput", checkInput);
$(document).on("keyup", "#emailInput", checkInput);
$(document).on("keyup", "#mobileNumberInput", checkInput);
$(document).on("keyup", "#aboutInput", checkInput);
$("#resumeUpload").change(checkInput);
