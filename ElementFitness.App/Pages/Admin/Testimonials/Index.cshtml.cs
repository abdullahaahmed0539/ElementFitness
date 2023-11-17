using ElementFitness.BL.Interfaces;
using ElementFitness.Models;
using ElementFitness.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Image = System.IO.File ;
using Serilog;
using Mapster;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class TestimonialsAdminViewModel : PageModel
    {

        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly ITestimonialService _testimonialService;
        public IEnumerable<Testimonial>? Testimonials { get; private set;}

        [BindProperty]
        public Testimonial? TestimonialToBeAdded { get; set;}

        [BindProperty]
        public Testimonial? UpdatedTestimonial { get; set; }

        public TestimonialsAdminViewModel(
            IWebHostEnvironment environment, 
            ITestimonialService testimonialService)
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _testimonialService = testimonialService;
        }

        public IActionResult OnGet()
        {
            try
            {
                Testimonials = _testimonialService.GetAll();
                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }


        public async Task<IActionResult> OnPost(IFormFile displayImg)
        {
            try
            {
                if (displayImg == null)
                    throw new UploadException("No display image is added. Please upload an image for the testimonial");

                Random randomizer = new Random();
                string randomizerNumber = "";
                for(int i = 0; i<3; i++)
                {
                    randomizerNumber +=  " " + randomizer.NextDouble().ToString(); 
                }

                string imgLink = Path.Combine(WWWRoot, $"lib/testimonials/{randomizerNumber}{displayImg.FileName}");
                using FileStream fileStream = new FileStream(imgLink, FileMode.Create);
                await displayImg.CopyToAsync(fileStream);

                TestimonialToBeAdded.ImageLink = $"~/lib/testimonials/{randomizerNumber}{displayImg.FileName}";
                try
                {
                    await _testimonialService.AddAsync(TestimonialToBeAdded)!;
                    TestimonialToBeAdded = null;
                    return RedirectToPage("./Index");
                }
                catch(Exception ex)
                {
                    Image.Delete(imgLink);
                    TestimonialToBeAdded = null;
                    throw new DatabaseException("An error occurred while adding the Testimonial. Please try again later.");
                }
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException || ex is InvalidModelException || ex is UploadException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async Task<IActionResult> OnPostUpdateTestimonial(int testimonialId, IFormFile imgToBeUpdated)
        {
            try
            {
                Testimonial? testimonialToBeUpdated = _testimonialService.GetById(testimonialId);
                testimonialToBeUpdated = UpdatedTestimonial.Adapt(testimonialToBeUpdated);
                FileStream stream = null;
                string imgLink = "";
                string file = "";
                if(imgToBeUpdated != null)
                {
                    file = Path.Combine(WWWRoot, $"lib/testimonials/{Path.GetFileName(testimonialToBeUpdated.ImageLink)}");
                    stream = new FileStream(file, FileMode.Open, FileAccess.Read);            
                    try
                    {
                        Image.Delete(file);
                    }
                    catch(Exception ex)
                    { 
                        Log.Error(ex.Message);
                        throw new UploadException("An error occurred while updating the testimonial. Please try again later."); 
                    }

                    Random randomizer = new Random();
                    string randomizerNumber = "";
                    for(int i = 0; i<3; i++)
                    {
                        randomizerNumber +=  " " + randomizer.NextDouble().ToString(); 
                    }
                    imgLink = Path.Combine(WWWRoot, $"lib/testimonials/{randomizerNumber}{imgToBeUpdated.FileName}");

                    try
                    {
                        using FileStream fileStream = new FileStream(imgLink, FileMode.Create);
                        await imgToBeUpdated.CopyToAsync(fileStream);
                    }
                    catch(Exception ex)
                    {
                        Log.Error(ex.Message);
                        using FileStream fileStream = new FileStream(file, FileMode.Create);
                        await stream?.CopyToAsync(fileStream);
                        throw new UploadException("An error occurred while updating the partners. Please try again later."); 
                    }
                    testimonialToBeUpdated.ImageLink = $"~/lib/testimonials/{randomizerNumber}{imgToBeUpdated.FileName}";
                }

                bool successfullyUpdated = await _testimonialService.UpdateAsync(testimonialToBeUpdated);
                if (!successfullyUpdated)
                {
                    if(imgToBeUpdated != null)
                    {
                        Image.Delete(imgLink);
                        using FileStream fileStream = new FileStream(file, FileMode.Create);
                        await stream?.CopyToAsync(fileStream);
                    }
                    throw new DatabaseException("An error occurred while updating the testimonial. Please refresh the page and try again later.");
                }
                
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException || ex is UploadException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }


        public async Task<IActionResult> OnPostDeleteTestimonial(int testimonialId)
        {
            try
            {
                Testimonial testimonialToBeDeleted = _testimonialService.GetById(testimonialId);
                string? imageLink = testimonialToBeDeleted?.ImageLink;
                bool successfullyDeleted = await _testimonialService.DeleteAsync(testimonialId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the testimonial. Please try again later.");
                string file = Path.Combine(WWWRoot, $"lib/testimonials/{Path.GetFileName(imageLink)}");
                Image.Delete(file);
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
    }
}