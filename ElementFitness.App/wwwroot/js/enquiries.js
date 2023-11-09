$(document).ready(() => {
    let msg = $("#error-message").val();
  if (msg.length > 0) {
    msg = msg.replace(/\\n/g, "\n");
    alert(msg);
    $("#error-message").attr("value", "");
  }
    
});


const viewMessage = async (
  enquiryId,
  read,
  firstname,
  lastname,
  message,
  objective,
  suitableTime,
  resolved
) => {

    read = read === 'False'? false: true
    if (read === false) {
    $.ajax({
      type: "POST",
      url: "/Admin/Enquiries?Handler=MarkAsRead",
      //   contentType: "application/json; charset=utf-8",
      //   dataType: "json",
      headers: {
        RequestVerificationToken: $(
          'input[name="__RequestVerificationToken"]'
        ).val(),
      },
      data: { enquiryId: enquiryId },
    });
  }

  $("#firstname").val(firstname);
  $("#lastname").val(lastname);
  $("#message").val(message);
  $("#objective").val(objective);
  $("#suitable-time").val(suitableTime);
    
  $("#resolve-modal-btn").attr("value", enquiryId);
  resolved == 'True' ? $("#resolve-modal-btn").attr("hidden", true): $("#resolve-modal-btn").attr("hidden", false)
  

};

const resetModal = () => {
  $("#firstname").val("");
  $("#lastname").val("");
  $("#message").val("");
  $("#objective").val("");
  $("#suitable-time").val("");
  window.location.reload();
};


const setOnDeleteParamValue = id => {
  $("#modal-delete-btn").attr("value", id);
};