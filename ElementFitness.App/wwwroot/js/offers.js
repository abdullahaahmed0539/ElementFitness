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

$("#ValidUptoInput").on("keyup", () => {
  const validUptoInput = $("#ValidUptoInput").val();
  $("#add-valid-upto-hint").text(`${validUptoInput.length}/30 characters`);
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

$("#validUptoInput").on("keyup", () => {
  const validUptoInput = $("#validUptoInput").val();
  $("#update-valid-upto-hint").text(`${validUptoInput.length}/30 characters`);
});

const resetAddModal = () => {
  $("#NameInput").val("");
  $("#DescriptionInput").val("");
  $("#ValidUptoInput").val("");
  $("#DisplayImgInput").val(null);
  $("#add-name-hint").text(`0/50 characters`);
  $("#add-description-hint").text(`0/400 characters`);
  $("#add-valid-upto-hint").text(`0/30 characters`);
};

const resetFields = (
  name,
  description,
  validUpto
) => {
  $("#nameInput").val(name);
  $("#descriptionInput").val(description);
  $("#validUptoInput").val(validUpto);
  $("#imgToBeUpdated").val(null);
  $("#update-name-hint").text(`${name.length}/50 characters`);
  $("#update-description-hint").text(`${description.length}/400 characters`);
  $("#update-valid-upto-hint").text(`${validUpto.length}/30 characters`);
  
};



const checkInput = () => {
  const nameInput = $("#NameInput").val().trim();
  const descriptionInput = $("#DescriptionInput").val().trim();
  const validUptoInput = $("#ValidUptoInput").val().trim();
  const imageUploaded = $("#DisplayImgInput")[0].files.length > 0;

  if (
    nameInput !== "" &&
    descriptionInput !== "" &&
    validUptoInput !== "" &&
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
$(document).on("keyup", "#ValidUptoInput", checkInput);
$("#DisplayImgInput").change(checkInput);