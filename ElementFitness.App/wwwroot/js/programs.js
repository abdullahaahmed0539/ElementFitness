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

  if (vars["enableEditing"] === "true")
  {
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

$("#DayAndTimingInput").on("keyup", () => {
  const dntInput = $("#DayAndTimingInput").val();
  $("#add-dnt-hint").text(`${dntInput.length}/50 characters`);
});

$("#OtherDetailsInput").on("keyup", () => {
  const otherInput = $("#OtherDetailsInput").val();
  $("#add-other-hint").text(`${otherInput.length}/100 characters`);
});

$("#TrainersInput").on("keyup", () => {
  const trainerInput = $("#TrainersInput").val();
  $("#add-trainer-hint").text(`${trainerInput.length}/100 characters`);
});

$("#ChargesInput").on("keyup", () => {
  const chargesInput = $("#ChargesInput").val();
  $("#add-charges-hint").text(`${chargesInput.length}/100 characters`);
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

$("#dayAndTimingInput").on("keyup", () => {
  const dntInput = $("#dayAndTimingInput").val();
  $("#update-dnt-hint").text(`${dntInput.length}/50 characters`);
});

$("#otherDetailsInput").on("keyup", () => {
  const otherInput = $("#otherDetailsInput").val();
  $("#update-other-hint").text(`${otherInput.length}/100 characters`);
});

$("#trainersInput").on("keyup", () => {
  const trainerInput = $("#trainersInput").val();
  $("#update-trainer-hint").text(`${trainerInput.length}/100 characters`);
});

$("#chargesInput").on("keyup", () => {
  const chargesInput = $("#chargesInput").val();
  $("#update-charges-hint").text(`${chargesInput.length}/100 characters`);
});

const resetAddModal = () => {
  $("#NameInput").val("");
  $("#DescriptionInput").val("");
  $("#DayAndTimingInput").val("");
  $("#OtherDetailsInput").val("");
  $("#TrainersInput").val("");
  $("#ChargesInput").val("");
  $("#DisplayImgInput").val(null);
  $("#add-name-hint").text(`0/50 characters`);
  $("#add-description-hint").text(`0/400 characters`);
  $("#add-dnt-hint").text(`0/50 characters`);
  $("#add-other-hint").text(`0/100 characters`);
  $("#add-trainer-hint").text(`0/100 characters`);
  $("#add-charges-hint").text(`0/100 characters`);
};

const resetFields = (name, description, dayAndTiming, otherDetails, trainers, charges) => {
  $("#nameInput").val(name);
  $("#descriptionInput").val(description);
  $("#dayAndTimingInput").val(dayAndTiming);
  $("#otherDetailsInput").val(otherDetails);
  $("#trainersInput").val(trainers);
  $("#chargesInput").val(charges);
  $("#imgToBeUpdated").val(null);
  $("#update-name-hint").text(`${name.length}/50 characters`);
  $("#update-description-hint").text(`${description.length}/400 characters`);
  $("#update-dnt-hint").text(`${dayAndTiming.length}/50 characters`);
  $("#update-other-hint").text(`${otherDetails.length}/100 characters`);
  $("#update-trainer-hint").text(`${trainers.length}/100 characters`);
  $("#update-charges-hint").text(`${charges.length}/100 characters`);
}


const checkInput = () => {
    const nameInput = $("#NameInput").val().trim();
    const descriptionInput = $("#DescriptionInput").val().trim();
    const dayAndTimingInput = $("#DayAndTimingInput").val().trim();
    const trainersInput = $("#TrainersInput").val().trim();
    const chargesInput = $("#ChargesInput").val().trim();
    const imageUploaded = $("#DisplayImgInput")[0].files.length > 0;

    if (
      nameInput !== "" &&
      descriptionInput !== "" &&
      dayAndTimingInput !== "" &&
      trainersInput !== "" &&
      chargesInput !== "" &&
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
$(document).on("keyup", "#DayAndTimingInput", checkInput);
$(document).on("keyup", "#TrainersInput", checkInput);
$(document).on("keyup", "#ChargesInput", checkInput);
$("#DisplayImgInput").change(checkInput);


