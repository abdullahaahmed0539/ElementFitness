using System.Diagnostics;
using ElementFitness.App.ViewModels;
using ElementFitness.BL.Interfaces;
using ElementFitness.Models;
using ElementFitness.Utils.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;

namespace ElementFitness.App.Pages
{
    [Authorize(Roles = "administrator")]
    public class MembersAndLeadsAdminViewModel : PageModel
    {

        private readonly IMemberAndLeadService _mnlService;
        public IEnumerable<Contact>? Leads { get; private set;}
        public IEnumerable<Contact>? Members { get; private set;}
        
        [BindProperty]
        public Contact ContactToBeAdded { get; set; }

        [BindProperty]
        public Contact ContactToBeUpdated { get; set; }


        public MembersAndLeadsAdminViewModel(IMemberAndLeadService mnlService)
        {
            _mnlService = mnlService;
        }

        public IActionResult OnGet()
        {
            try
            {
                IEnumerable<Contact>? contacts = _mnlService.GetAll();
                Leads = contacts.Where(c => c.ContactType?.ToLower() == "lead");
                Members = contacts.Where(c => c.ContactType?.ToLower() == "member");
                return Page();
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToPage("../../Error");
            }
        
        }


        public async Task<IActionResult> OnPost()
        {
            try
            {
                ContactToBeAdded.ContactType = ContactToBeAdded.ContactType.ToLower();
                if (ContactToBeAdded.ContactType == "member")
                    ContactToBeAdded.ConvertedOn = DateTime.UtcNow;

                await _mnlService.AddAsync(ContactToBeAdded);
                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                ViewData["ErrorMessage"] = ex.Message;
                return OnGet();
            }
        }

        public async Task<IActionResult> OnPostConvertContact(int contactId)
        {
            try
            {
                Contact? contact = _mnlService.GetById(contactId);
                if(contact != null)
                {
                    contact.ContactType = "member";
                    try
                    {
                        await _mnlService.UpdateAsync(contact);
                        return RedirectToPage("./Index"); 
                    }
                    catch(Exception ex)
                    {
                        throw new DatabaseException("An error occurred while converting the contact to member. Please try again later.");
                    }
                }
                throw new NullReferenceException($"No such contact exist with the ID = {contactId}");
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

        public async Task<IActionResult> OnPostUpdateContact(int contactId)
        {
            try
            {
                Contact? contactToBeUpdated = _mnlService.GetById(contactId);
                if(contactToBeUpdated != null)
                {
                    contactToBeUpdated = ContactToBeUpdated.Adapt(contactToBeUpdated);
                    try
                    {
                        await _mnlService.UpdateAsync(contactToBeUpdated);
                        return RedirectToPage("./Index"); 
                    }
                    catch(Exception ex)
                    {
                        throw new DatabaseException("An error occurred while converting the contact to member. Please try again later.");
                    }
                }
                throw new NullReferenceException($"No such contact exist with the ID = {contactId}");
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



        public async Task<IActionResult> OnPostDeleteContact(int contactId)
        {
            try
            {
                bool successfullyDeleted = await _mnlService.DeleteAsync(contactId);
                if (!successfullyDeleted)
                    throw new DatabaseException("An error occurred while deleting the contact. Please try again later.");
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