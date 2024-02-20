using ElementFitness.BL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ElementFitness.App.Pages
{
    public class AboutUsModel : PageModel
    {

        private readonly IProgramService _programService;

        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        public IEnumerable<Models.Program>? Programs { get; private set;}
        public string[]? PhotosPath { get; private set; }
        public bool PhotosExist { get; private set; }

        public AboutUsModel(IProgramService programService, IWebHostEnvironment environment)
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
            _programService = programService;
        }

        public void OnGet()
        {
            try
            {
                Programs = _programService.GetAll();
                string directory = Path.Combine(WWWRoot, $"lib/facilities");
                string[] files = Directory.GetFiles(directory);
                PhotosPath = new string[files.Length];
                if(files.Length > 0)
                {
                    PhotosExist = true;
                    for(int i=0; i<files.Length; i++)
                    {
                        PhotosPath[i] = "lib/facilities/" + Path.GetFileName(files[i]);
                    }
                }
                else
                {
                    PhotosExist = false;
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}



                
