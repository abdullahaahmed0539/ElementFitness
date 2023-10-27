$(document).ready(() => {
  let msg = $("#error-message").val();
  if (msg.length > 0) {
    msg = msg.replace(/\\n/g, "\n");
    alert(msg);
    $("#error-message").attr("value", "");
  }

  let vars = [],
    hash;
  let hashes = window.location.href
    .slice(window.location.href.indexOf("?") + 1)
    .split("&");
  for (var i = 0; i < hashes.length; i++) {
    hash = hashes[i].split("=");
    vars.push(hash[0]);
    vars[hash[0]] = hash[1];
  }

  if (vars["enableEditing"] === "true") {
    $("#update-modal").modal("show");
  }
});



$("#FirstNameInput").on("keyup", () => {
  const firstNameInput = $("#FirstNameInput").val();
  $("#add-firstname-hint").text(`${firstNameInput.length}/50 characters`);
});

$("#LastNameInput").on("keyup", () => {
  const lastNameInput = $("#LastNameInput").val();
  $("#add-lastname-hint").text(`${lastNameInput.length}/50 characters`);
});

$("#DescriptionInput").on("keyup", () => {
  const descriptionInput = $("#DescriptionInput").val();
  $("#add-description-hint").text(`${descriptionInput.length}/400 characters`);
});

$("#JobTitleInput").on("keyup", () => {
  const jobTitleInput = $("#JobTitleInput").val();
  $("#add-jobtitle-hint").text(`${jobTitleInput.length}/100 characters`);
});

$("#firstNameInput").on("keyup", () => {
  const nameInput = $("#firstNameInput").val();
  $("#update-firstname-hint").text(`${nameInput.length}/50 characters`);
});

$("#lastNameInput").on("keyup", () => {
  const nameInput = $("#lastNameInput").val();
  $("#update-lastname-hint").text(`${nameInput.length}/50 characters`);
});

$("#jobTitleInput").on("keyup", () => {
  const jobTitleInput = $("#jobTitleInput").val();
  $("#update-jobtitle-hint").text(`${jobTitleInput.length}/100 characters`);
});

$("#descriptionInput").on("keyup", () => {
  const descriptionInput = $("#descriptionInput").val();
  $("#update-description-hint").text(
    `${descriptionInput.length}/400 characters`
  );
});


const resetAddModal = () => {
    $("#FirstNameInput").val("");
    $("#LastNameInput").val("");
    $("#DescriptionInput").val("");
    $("#JobTitleInput").val("");
    $("#DisplayImgInput").val(null);
    $("#add-firstname-hint").text(`0/50 characters`);
    $("#add-lastname-hint").text(`0/50 characters`);
    $("#add-jobtitle-hint").text(`0/100 characters`);
    $("#add-description-hint").text(`0/400 characters`);
};

const resetFields = (firstname, lastname, bio, jobTitle) => {
    $("#firstNameInput").val(firstname);
    $("#lastNameInput").val(lastname);
    $("#jobTitleInput").val(jobTitle);
    $("#descriptionInput").val(bio);
    $("#imgToBeUpdated").val(null);
    $("#update-firstname-hint").text(`${firstname.length}/50 characters`);
    $("#update-lastname-hint").text(`${lastname.length}/50 characters`);
    $("#update-jobtitle-hint").text(`${jobTitle.length}/100 characters`);
    $("#update-description-hint").text(`${bio.length}/400 characters`);
    
};

const checkInput = () => {
    const firstNameInput = $("#FirstNameInput").val().trim();
    const lastNameInput = $("#LastNameInput").val().trim();
    const jobTitleInput = $("#JobTitleInput").val().trim();
    const descriptionInput = $("#DescriptionInput").val().trim();
    const imageUploaded = $("#DisplayImgInput")[0].files.length > 0;

  if (
    firstNameInput !== "" &&
    lastNameInput !== "" &&
    descriptionInput !== "" &&
    jobTitleInput !== "" &&
    imageUploaded
  ) {
    $("#disabled-add-modal-add-btn").attr("hidden", true);
    $("#add-modal-add-btn").attr("hidden", false);
  } else {
    $("#disabled-add-modal-add-btn").attr("hidden", false);
    $("#add-modal-add-btn").attr("hidden", true);
  }
};

const setOnDeleteParamValue = id => {
  $("#modal-delete-btn").attr("value", id);
};

$(document).on("keyup", "#FirstNameInput", checkInput);
$(document).on("keyup", "#LastNameInput", checkInput);
$(document).on("keyup", "#JobTitleInput", checkInput);
$(document).on("keyup", "#DescriptionInput", checkInput);
$("#DisplayImgInput").change(checkInput);