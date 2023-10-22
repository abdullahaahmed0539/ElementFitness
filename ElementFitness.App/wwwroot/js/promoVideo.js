$(document).ready(() => {
  let msg = $("#error-message").val();
  if (msg.length > 0) {
    alert(msg);
    $("#error-message").attr("value", "");
  }
});

$("#replaceVideo").change(() => {
  if ($("#replaceVideo")[0].files.length > 0) {
    $("#disabled-upload-button").attr("hidden", true);
    $("#upload-button").attr("hidden", false);
  } else {
    $("#disabled-upload-button").attr("hidden", false);
    $("#upload-button").attr("hidden", true);
  }
});
