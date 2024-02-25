using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    public class TestimonialModel : PageModel
    {
        private readonly ITestimonialService _testimonialService;
        public IEnumerable<Models.Testimonial>? Testimonials { get; private set; }
        public TestimonialModel(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public void OnGet()
        {
            try
            {
                Testimonials = _testimonialService.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}