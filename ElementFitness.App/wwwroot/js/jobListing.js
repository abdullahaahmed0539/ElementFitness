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


$("#JobTitleInput").on("keyup", () => {
  const jobTitleInput = $("#JobTitleInput").val();
  $("#add-jobtitle-hint").text(`${jobTitleInput.length}/50 characters`);
});


$("#jobTitleInput").on("keyup", () => {
  const nameInput = $("#jobTitleInput").val();
  $("#update-jobtitle-hint").text(`${nameInput.length}/50 characters`);
});


const resetAddModal = () => {
  $("#JobTitleInput").val("");
  $("#DescriptionInput").val("");
  $("#RequirementInput").val("");
  $("#add-jobtitle-hint").text(`0/50 characters`);
};

const resetFields = (jobtitle, description, requirements) => {
  $("#jobTitleInput").val(jobtitle);
  $("#descriptionInput").val(description);
  $("#requirementInput").val(requirements);
  $("#update-jobtitle-hint").text(`${jobtitle.length}/50 characters`);
};

const checkInput = () => {
  const jobTitleInput = $("#JobTitleInput").val().trim();
  const requirementInput = $("#RequirementInput").val().trim();
  const descriptionInput = $("#DescriptionInput").val().trim();

  if (
    jobTitleInput !== "" &&
    descriptionInput !== "" &&
    requirementInput !== ""
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

const archiveJobListing = id => {
  $("#modal-archive-btn").attr("value", id);
}

const unarchiveJobListing = id => {
  $("#modal-unarchive-btn").attr("value", id);
};

$(document).on("keyup", "#JobTitleInput", checkInput);
$(document).on("keyup", "#RequirementInput", checkInput);
$(document).on("keyup", "#DescriptionInput", checkInput);
$("#DisplayImgInput").change(checkInput);



