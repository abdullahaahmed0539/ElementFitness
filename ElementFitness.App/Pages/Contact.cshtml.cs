﻿using ElementFitness.BL.Interfaces;
using ElementFitness.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using ElementFitness.Models;

namespace ElementFitness.App.Pages
{

    public class ContactModel : PageModel
    {
        private readonly IEnquiryService _enquiryService;
        [BindProperty]
        public Enquiry? enquiryToBeAdded { get; set; }
        public ContactModel(IEnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        public IActionResult OnGet()
        {
            try
            {
                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    IEnumerable<string> allErrors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
                    string errorMessage = "";
                    foreach (string error in allErrors)
                    {
                        errorMessage += $"•{error} \\n";
                    }
                    throw new InvalidModelException(errorMessage);
                }

                try
                {
                    await _enquiryService.AddAsync(enquiryToBeAdded)!;
                    enquiryToBeAdded = null;
                    return RedirectToPage("./Index");
                }
                catch (Exception ex)
                {
                    enquiryToBeAdded = null;
                    throw new DatabaseException($"An error occurred while adding the Partner. Please try again later.");
                }
            }
            catch (Exception ex)
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
    }
}