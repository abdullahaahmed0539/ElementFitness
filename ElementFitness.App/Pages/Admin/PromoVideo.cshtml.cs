using ElementFitness.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class PromoVideoAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        public bool VideoExists { get; private set;}
        public string? VideoPath { get; private set; }

        public PromoVideoAdminViewModel(IWebHostEnvironment environment)
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
        }

        public IActionResult OnGet()
        {
            try
            {

                string directory = Path.Combine(WWWRoot, $"lib/promoVideo");
                if(Directory.GetFiles(directory).Length > 0)
                {
                    VideoExists = true;
                    string fileName = Path.GetFileName(Directory.GetFiles(directory)[0]);
                    VideoPath = $"lib/promoVideo/{fileName}";
                }
                else
                {
                    VideoExists = false;
                    VideoPath = ""; 
                }
                return Page();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../Error");
            }
        }

        public async Task<IActionResult> OnPost(IFormFile videoToBeReplaced)
        {
            try
            {
                if (videoToBeReplaced == null)
                    throw new Exception("No file chosen");

                string filename = Randomizer.GenerateRandomName();
                string directory = Path.Combine(WWWRoot, $"lib/promoVideo");
                string videoPath = Path.Combine(WWWRoot, $"lib/promoVideo/{filename}.mp4");
                if(Directory.GetFiles(directory).Length > 0)
                {
                    DirectoryInfo dir = new DirectoryInfo(directory);
                    foreach (FileInfo fi in dir.GetFiles())
                        fi.Delete();
                }
                using (FileStream fileStream = new FileStream(videoPath, FileMode.Create))
                { 
                    await videoToBeReplaced.CopyToAsync(fileStream);
                }
                return OnGet();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../Error");
            }
        }
    }
}