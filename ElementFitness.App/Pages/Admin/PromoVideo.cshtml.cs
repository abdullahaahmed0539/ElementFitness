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
    public class PromoVideoAdminViewModel : PageModel
    {
        public PromoVideoAdminViewModel()
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