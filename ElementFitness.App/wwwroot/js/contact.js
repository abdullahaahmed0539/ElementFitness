const generalenqueries = () => {
    $("#program-block").attr("hidden", true);
    $("#map-detail-block").attr("hidden", true);
    $("#general-enquiry-block").attr("hidden", false);
}

$("#FirstNameInput").on("keyup", () => {
    const firstnameInput = $("#FirstNameInput").val();
    $("#add-first-name-hint").text(`${firstnameInput.length}/50 characters`);
});

$("#LastNameInput").on("keyup", () => {
    const lastnameInput = $("#LastNameInput").val();
    $("#add-last-name-hint").text(`${lastnameInput.length}/50 characters`);
});

$("#EmailInput").on("keyup", () => {
    const emailInput = $("#EmailInput").val();
    $("#add-email-hint").text(`${emailInput.length}/100 characters`);
});

$("#MobileNumberInput").on("keyup", () => {
    const mobileInput = $("#MobileNumberInput").val();
    $("#add-mobile-number-hint").text(`${mobileInput.length}/50 characters`);
});

$("#MessageInput").on("keyup", () => {
    const messageInput = $("#MessageInput").val();
    $("#add-message-hint").text(`${messageInput.length}/400 characters`);
});

const checkInput = () => {
    const firstnameInput = $("#FirstNameInput").val().trim();
    const lastnameInput = $("#LastNameInput").val().trim();
    const emailInput = $("#EmailInput").val().trim();
    const mobileNumberInput = $("#MobileNumberInput").val().trim();

    if (
        firstnameInput !== "" &&
        lastnameInput !== "" &&
        emailInput !== "" &&
        mobileNumberInput !== ""
    ) {
        $("#disabled-add-modal-add-btn").attr("hidden", true);
        $("#add-modal-add-btn").attr("hidden", false);
    } else {
        $("#disabled-add-modal-add-btn").attr("hidden", false);
        $("#add-modal-add-btn").attr("hidden", true);
    }
};