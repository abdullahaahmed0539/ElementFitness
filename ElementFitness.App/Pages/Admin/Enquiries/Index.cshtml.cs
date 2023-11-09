using Image = System.IO.File ;
using ElementFitness.BL.Interfaces;
using ElementFitness.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using ElementFitness.Models;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class EnquiriesIndexAdminViewModel : PageModel
    {
        private readonly IEnquiryService _enquiryService;
        public IEnumerable<Enquiry>? UnreadEnquiries { get; private set;}
        public IEnumerable<Enquiry>? UnrespondedEnquiries { get; private set;}
        public IEnumerable<Enquiry>? RespondedEnquiries { get; private set;}

        public EnquiriesIndexAdminViewModel(
            IEnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        public IActionResult OnGet()
        {
            try
            {
                IEnumerable<Enquiry>? enquiries = _enquiryService.GetAll();
                UnreadEnquiries = enquiries.Where(e => e.Read == false);
                UnrespondedEnquiries = enquiries.Where(e => e.Read == true && e.Resolved == false);
                RespondedEnquiries = enquiries.Where(e => e.Read == true && e.Resolved == true);
                return Page();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }

        public async void OnPostMarkAsRead(int enquiryId)
        {
            try
            {
                Enquiry? enquiry = _enquiryService.GetById(enquiryId);
                if(enquiry != null)
                {
                    enquiry.Read = true;
                    await _enquiryService.UpdateAsync(enquiry);
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
            
        }

        public async Task<IActionResult> OnPostMarkAsResolved(int enquiryID)
        {
            try
            {
                Enquiry? enquiry = _enquiryService.GetById(enquiryID);
                if(enquiry != null)
                {
                    enquiry.Resolved = true;
                    try
                    {
                        await _enquiryService.UpdateAsync(enquiry);
                        return RedirectToPage("./Index"); 
                    }
                    catch(Exception ex)
                    {
                        throw new DatabaseException("An error occurred while marking the enquiry as read. Please try again later.");
                    }
                }
                throw new NullReferenceException($"No such enquiry exist with the ID = {enquiryID}");
            }
            catch(Exception ex)
            {
                if (ex is DatabaseException || ex is NullReferenceException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    Log.Error(ViewData["ErrorMessage"].ToString());
                    return OnGet();
                }

                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        }


        public async Task<IActionResult> OnPostDeleteEnquiry(int enquiryId)
        {
            try
            {
                Enquiry? offerToBeDeleted = _enquiryService.GetById(enquiryId);
                bool successfullyDeleted = await _enquiryService.DeleteAsync(enquiryId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the enquiry. Please try again later.");
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