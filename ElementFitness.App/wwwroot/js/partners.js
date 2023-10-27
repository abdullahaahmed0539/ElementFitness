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

$("#NameInput").on("keyup", () => {
  const nameInput = $("#NameInput").val();
  $("#add-name-hint").text(`${nameInput.length}/50 characters`);
});

$("#DescriptionInput").on("keyup", () => {
  const descriptionInput = $("#DescriptionInput").val();
  $("#add-description-hint").text(`${descriptionInput.length}/400 characters`);
});

$("#nameInput").on("keyup", () => {
  const nameInput = $("#nameInput").val();
  $("#update-name-hint").text(`${nameInput.length}/50 characters`);
});

$("#descriptionInput").on("keyup", () => {
  const descriptionInput = $("#descriptionInput").val();
  $("#update-description-hint").text(
    `${descriptionInput.length}/400 characters`
  );
});


const resetAddModal = () => {
  $("#NameInput").val("");
  $("#DescriptionInput").val("");
  $("#DisplayImgInput").val(null);
  $("#add-name-hint").text(`0/50 characters`);
  $("#add-description-hint").text(`0/400 characters`);
};

const resetFields = (name, description) => {
  $("#nameInput").val(name);
  $("#descriptionInput").val(description);
  $("#imgToBeUpdated").val(null);
  $("#update-name-hint").text(`${name.length}/50 characters`);
  $("#update-description-hint").text(`${description.length}/400 characters`);
};

const checkInput = () => {
  const nameInput = $("#NameInput").val().trim();
  const descriptionInput = $("#DescriptionInput").val().trim();
  const imageUploaded = $("#DisplayImgInput")[0].files.length > 0;

  if (
    nameInput !== "" &&
    descriptionInput !== "" &&
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

$(document).on("keyup", "#NameInput", checkInput);
$(document).on("keyup", "#DescriptionInput", checkInput);
$("#DisplayImgInput").change(checkInput);
