$(document).ready(() => {
    let msg = $("#error-message").val();
    if (msg.length > 0) {
        msg = msg.replace(/\\n/g, "\n");
        alert(msg);
        $("#error-message").attr("value", "");
    }
});



const checkInput = () => {
    const newFacebooklink = $("#FacebookLinkInput").val().trim();

    if (
        newFacebooklink !== ""
    ) {
        $("#disabled-change-btn").attr("hidden", true);
        $("#change-btn").attr("hidden", false);
    } else {
        $("#disabled-change-btn").attr("hidden", false);
        $("#change-btn").attr("hidden", true);
    }
};

const checkInput2 = () => {
    const newInstaLink = $("#InstaLinkInput").val().trim();

    if (
        newInstaLink !== ""
    ) {
        $("#disabled-change-btn-insta").attr("hidden", true);
        $("#change-btn-insta").attr("hidden", false);
    } else {
        $("#disabled-change-btn-insta").attr("hidden", false);
        $("#change-btn-insta").attr("hidden", true);
    }
};


$(document).on("keyup", "#FacebookLinkInput", checkInput);
$(document).on("keyup", "#InstaLinkInput", checkInput2);