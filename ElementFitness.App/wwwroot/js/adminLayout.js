let sidebarDisplaying = true;



$(document).ready(() => {
  const splitURL = $(location).attr('href').replace(/\/\s*$/, "").split('/');
  const page = splitURL[splitURL.indexOf("Admin") + 1];
    
    
    if (page.startsWith("PromoVideo")) $("#PromoVideo").addClass("active");
    else if (page.startsWith("Facilities")) $("#Facilities").addClass("active");
    else if (page.startsWith("Programs")) $("#Programs").addClass("active");
    else if (page.startsWith("Offers")) $("#Offers").addClass("active");
    else if (page.startsWith("Trainers")) $("#Trainers").addClass("active");
    else if (page.startsWith("Partners")) $("#Partners").addClass("active");
    else if (page.startsWith("Enquiries")) $("#Enquiries").addClass("active");
    else if (page.startsWith("JobListings")) $("#JobListings").addClass("active");
    else if (page.startsWith("JobApplications")) $("#JobApplications").addClass("active");
    else if (page.startsWith("Members&Leads")) $("#Members").addClass("active");
    else if (page.startsWith("Testimonials")) $("#Testimonials").addClass("active");
    else $("#PromoVideo").addClass("active");
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
 });


 $("#secondary-sidebar-toggle-button").on("click", () => {
   if (sidebarDisplaying) {
     $(".sidebar").removeClass("active");
     sidebarDisplaying = false;
   } else {
     $(".sidebar").addClass("active");
     sidebarDisplaying = true;
   }
 });


const logout = () => {
    const url = window.location.pathname;
    window.location.replace(`../Account/Logout?returnUrl=${url}`);
}


