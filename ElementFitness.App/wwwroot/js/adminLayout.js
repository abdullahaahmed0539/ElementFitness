let sidebarDisplaying = true;



$(document).ready(() => {
    const page = $(location).attr('href').replace(/\/\s*$/, "").split('/').pop();
    switch(page){
        case 'PromoVideo':
            $("#PromoVideo").addClass('active');
            break;
        case 'Facilities':
            $("#Facilities").addClass('active');
            break;
        case 'Programs':
            $("#Programs").addClass('active');
            break;
        case 'Offers':
            $("#Offers").addClass('active');
            break;
        case 'Trainers':
            $("#Trainers").addClass('active');
            break;
        case 'Partners':
            $("#Partners").addClass('active');
            break;
        case 'Enquiries':
            $("#Enquiries").addClass('active');
            break;
        case 'JobListings':
            $("#JobListings").addClass('active');
            break;
        case 'JobApplications':
            $("#JobApplications").addClass('active');
            break;
        case 'Members&Leads':
            $("#Members").addClass('active');
            break;
        default:
            $("#PromoVideo").addClass('active');
    }

})

$("#sidebar-toggle-button").on("click", () => {
    if (sidebarDisplaying)
    {
        $(".sidebar").removeClass("active")
        sidebarDisplaying = false;
    }
    else
    {
        $(".sidebar").addClass("active"); 
        sidebarDisplaying = true;
    }
    console.log(sidebarDisplaying);

 });


const logout = () => {
    const url = window.location.pathname;
    window.location.replace(`../Account/Logout?returnUrl=${url}`);
}


