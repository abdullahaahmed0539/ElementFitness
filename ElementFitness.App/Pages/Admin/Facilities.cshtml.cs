﻿using ElementFitness.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Image = System.IO.File ;
using Serilog;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class FacilitiesAdminViewModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string WWWRoot;
        public bool PhotosExist { get; private set;}
        public string[]? PhotosPath { get; private set; }



        public FacilitiesAdminViewModel(IWebHostEnvironment environment)
        {
            _environment = environment;
            WWWRoot = _environment.WebRootPath;
        }

        public IActionResult OnGet()
        {
            try
            {

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
                return Page();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../Error");
            }
        }

        public  async Task<IActionResult> OnPost(List<IFormFile> imagesToBeUploaded)
        {
            try
            {
                if (imagesToBeUploaded == null)
                    throw new Exception("No file chosen");                             

                string[] filenames = Randomizer.GenerateRandomNames(imagesToBeUploaded.Count);
                string directory = Path.Combine(WWWRoot, $"lib/facilities");

                if (Directory.GetFiles(directory).Length + imagesToBeUploaded.Count > 8)
                    throw new Exception("Image count can not be greater than 8");

                string[] imagePaths = new string[filenames.Length];
                string fileExtension;
                for(int i=0; i<filenames.Length; i++)
                {
                    fileExtension = Path.GetExtension(imagesToBeUploaded.ElementAt(i).FileName);
                    imagePaths[i] =  Path.Combine(WWWRoot, $"lib/facilities/{filenames[i]}{fileExtension}");
                }

                for(int i = 0; i<imagePaths.Length; i++)
                {
                    using FileStream fileStream = new FileStream(imagePaths[i], FileMode.Create);
                    await imagesToBeUploaded.ElementAt(i).CopyToAsync(fileStream);
                }

                return OnGet();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../Error");
            }
        }
    
        public IActionResult OnPostDeleteImage(string imagePath)
        {
            try
            {
                string directory = Path.Combine(WWWRoot, $"lib/facilities/{Path.GetFileName(imagePath)}");
                Image.Delete(directory);
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