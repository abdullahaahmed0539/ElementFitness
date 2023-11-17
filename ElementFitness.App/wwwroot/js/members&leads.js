$(document).ready(() => {

    // $("#memberTable").DataTable();

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

const setOnDeleteParamValue = id => {
  $("#modal-delete-btn").attr("value", id);
};

const setOnConvertParamValue = id => {
  $("#modal-convert-btn").attr("value", id);
};


$("#firstNameInput").on("keyup", () => {
  const firstNameInput = $("#firstNameInput").val();
  $("#add-firstname-hint").text(`${firstNameInput.length}/50 characters`);
});

$("#lastNameInput").on("keyup", () => {
  const lastNameInput = $("#lastNameInput").val();
  $("#add-lastname-hint").text(`${lastNameInput.length}/50 characters`);
});

$("#professionInput").on("keyup", () => {
  const professionInput = $("#professionInput").val();
  $("#add-profession-hint").text(`${professionInput.length}/50 characters`);
});

$("#FirstNameInput").on("keyup", () => {
  const firstNameInput = $("#FirstNameInput").val();
  $("#updated-firstname-hint").text(`${firstNameInput.length}/50 characters`);
});

$("#LastNameInput").on("keyup", () => {
  const lastNameInput = $("#LastNameInput").val();
  $("#updated-lastname-hint").text(`${lastNameInput.length}/50 characters`);
});

$("#ProfessionInput").on("keyup", () => {
  const professionInput = $("#ProfessionInput").val();
  $("#updated-profession-hint").text(`${professionInput.length}/50 characters`);
});

const resetAddModal = () => {
    $("#firstNameInput").val("");
    $("#lastNameInput").val("");
    $("#emailInput").val("");
    $("#phoneNumberInput").val("");
    $("#professionInput").val("");
    $("#ContactToBeAdded_ContactType").prop("selectedIndex", 0);
    $("#add-firstname-hint").text(`0/50 characters`);
    $("#add-lastname-hint").text(`0/50 characters`);
    $("#add-profession-hint").text(`0/50 characters`);
};

const setFields = (firstname, lastname, email, phone, profession, id, contactType) => {
    $("#FirstNameInput").val(firstname);
    $("#LastNameInput").val(lastname);
    $("#EmailInput").val(email);
    $("#PhoneNumberInput").val(phone);
    $("#ProfessionInput").val(profession);
    if (contactType.toLowerCase() === 'member') {
        $("#ContactToBeUpdated_ContactType").prop("selectedIndex", 0);
    }
    else {
        $("#ContactToBeUpdated_ContactType").prop("selectedIndex", 1);
    }
    $("#updated-firstname-hint").text(`${firstname.length}/50 characters`);
    $("#updated-lastname-hint").text(`${lastname.length}/50 characters`);
    $("#updated-profession-hint").text(`${profession.length}/50 characters`);
    $("#update-modal-add-btn").attr("value", id);
    ;
}


const resetModal = () => {
  $("#FirstNameInput").val("");
  $("#LastNameInput").val("");
  $("#EmailInput").val("");
  $("#PhoneNumberInput").val("");
  $("#ProfessionInput").val("");
  $("#ContactToBeUpdated_ContactType").prop("selectedIndex", 0);
  $("#updated-firstname-hint").text(`0/50 characters`);
  $("#updated-lastname-hint").text(`0/50 characters`);
  $("#updated-profession-hint").text(`0/50 characters`);
};

const resetFields = (jobtitle, description, requirements) => {
  $("#jobTitleInput").val(jobtitle);
  $("#descriptionInput").val(description);
  $("#requirementInput").val(requirements);
  $("#update-jobtitle-hint").text(`${jobtitle.length}/50 characters`);
};

const checkInput = () => {
    const firstNameInput = $("#firstNameInput").val().trim();
    const lastNameInput = $("#lastNameInput").val().trim();
  if (
    firstNameInput !== "" &&
    lastNameInput !== "" 
  ) {
    $("#disabled-add-modal-add-btn").attr("hidden", true);
    $("#add-modal-add-btn").attr("hidden", false);
  } else {
    $("#disabled-add-modal-add-btn").attr("hidden", false);
    $("#add-modal-add-btn").attr("hidden", true);
  }
};




$(document).on("keyup", "#firstNameInput", checkInput);
$(document).on("keyup", "#lastNameInput", checkInput);
