using System.Diagnostics;
using ElementFitness.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class MembersAndLeadsAdminViewModel : PageModel
    {
        public MembersAndLeadsAdminViewModel()
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