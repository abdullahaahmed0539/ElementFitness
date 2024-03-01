using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using ElementFitness.Utils.Configurations;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace ElementFitness.BL.Services
{
    public class EnquiryService: IEnquiryService
    {
        private readonly IEnquiryRepo _enquiryRepo;

        public EnquiryService(IEnquiryRepo enquiryRepo)
        {
            _enquiryRepo = enquiryRepo;
        }

        public async void SendEnquiryEmail(Enquiry enquiry)
        {
            Console.WriteLine(enquiry.Contact.Email);
            Console.WriteLine(AppSettings.MailjetReceiverEmail);

            MailjetClient client = new MailjetClient(
                AppSettings.MailjetPublicKey,
                AppSettings.MailjetPrivateKey
                );


            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", AppSettings.MailjetSystemEmail},
                  {"Name", "System Email"}
                  }},

                 {"To", new JArray {
                  new JObject {
                   {"Email", AppSettings.MailjetReceiverEmail},
                   }
                  }},

                 {"Subject", $"New Enquiry by {enquiry.Contact.FirstName} {enquiry.Contact.LastName}"},
                 {"TextPart", $"There is a new enquiry by {enquiry.Contact.FirstName} {enquiry.Contact.LastName}. \n\nThe Enquiry Message is: \n\n {enquiry.Message}. \n\nHere are the details of the person: \n\n Email: {enquiry.Contact.Email} \n\n Mobile Number: {enquiry.Contact.MobileNumber} \n\n\n"},
                 }
                });


            MailjetResponse response = await client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.GetErrorMessage());
        }

        public async Task<Enquiry>? AddAsync(Enquiry newInstance)
        {
            return await _enquiryRepo.AddAsync(newInstance);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _enquiryRepo.DeleteAsync(id);
        }

        public IEnumerable<Enquiry>? GetAll()
        {
            return _enquiryRepo.GetAll();
        }

        public Enquiry? GetById(int id)
        {
            return _enquiryRepo.GetByCondition(e => e.EnquiryID == id);
        }

        public async Task<bool> UpdateAsync(Enquiry updatedObj)
        {
            return await _enquiryRepo.UpdateAsync(updatedObj);
        }
    }
}