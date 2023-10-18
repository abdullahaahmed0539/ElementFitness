$("#replaceVideo").change(() => {
  if ($("#replaceVideo")[0].files.length > 0) {
    $("#disabled-upload-button").attr("hidden", true);
    $("#upload-button").attr("hidden", false);
  } else {
     $("#disabled-upload-button").attr("hidden", false);
     $("#upload-button").attr("hidden", true);
  }
});


