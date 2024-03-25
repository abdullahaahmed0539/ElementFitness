$(document).ready(() => {
  const splitURL = $(location)
    .attr("href")
    .replace(/\/\s*$/, "")
    .split("/");
  // const page = splitURL[splitURL.length - 1];
  let page = splitURL[3];
  if (page === undefined)
    page = "Home"

    if (page.startsWith("Home")) $("#home-link").addClass("half-a-border-on-top");
    else if (page.startsWith("AboutUs"))
        $("#about-us-link").addClass("half-a-border-on-top");
    else if (page.startsWith("Trainers"))
        $("#trainers-link").addClass("half-a-border-on-top");
    else if (page.startsWith("Memberships"))
        $("#memberships-link").addClass("half-a-border-on-top");
    else if (page.startsWith("Partners"))
        $("#partners-link").addClass("half-a-border-on-top");
    else if (page.startsWith("Testimonials"))
        $("#testimonials-link").addClass("half-a-border-on-top");
    else if (page.startsWith("Careers")) {
        $("#careers-link").addClass("half-a-border-on-top");
        //document.querySelector('.footer').style.position = "absolute";
        //document.querySelector('.footer').style.width = "100%";
        //document.querySelector('.footer').style.bottom = "0";
    }
    else if (page.startsWith("Contact"))
    $("#contact-link").addClass("half-a-border-on-top");
    else $("#home-link").addClass("half-a-border-on-top");
});
