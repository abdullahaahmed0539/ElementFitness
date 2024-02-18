using ElementFitness.BL.Interfaces;
using ElementFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        private readonly ITestimonialService _testimonialService;

        public string VideoPath { get; private set; }

        public IEnumerable<Testimonial>? Testimonials { get; private set;}


        public IndexModel(IWebHostEnvironment environment, ITestimonialService testimonialService)
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _testimonialService = testimonialService;
        }

        public void OnGet()
        {
            try
            {
                string directory = Path.Combine(WWWRoot, $"lib/promoVideo");
                string fileName = Path.GetFileName(Directory.GetFiles(directory)[0]);
                VideoPath = $"~/lib/promoVideo/{fileName}";

                Testimonials = _testimonialService.GetNRecords(4);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}