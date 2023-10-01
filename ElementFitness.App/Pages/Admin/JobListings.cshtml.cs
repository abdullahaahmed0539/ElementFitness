﻿using System.Diagnostics;
using ElementFitness.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class JobListingsAdminViewModel : PageModel
    {
        public JobListingsAdminViewModel()
        {
        }

        public void OnGet()
        {
        
        }

        public void OnPost()
        {
        }
    }
}