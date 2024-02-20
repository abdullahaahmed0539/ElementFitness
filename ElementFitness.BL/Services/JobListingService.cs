using ElementFitness.BL.Interfaces;
using ElementFitness.DAL.Interfaces;
using ElementFitness.Models;
using ElementFitness.Utils;
using ElementFitness.Utils.Configurations;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace ElementFitness.BL.Services
{
    public class JobListingService: IJobListingService
    {
        private readonly IJobListingRepo _jobListingRepo;

        public JobListingService(IJobListingRepo jobListingRepo)
        {
            _jobListingRepo = jobListingRepo;
        }

        public async Task<Job>? AddAsync(Job newInstance)
        {
            return await _jobListingRepo.AddAsync(newInstance);
        }

        public async void SendProfileByEmail(JobApplicant jobApplicant, MemoryStream resumeUpload, string jobTitle)
        {

            Console.WriteLine(jobApplicant.Email);
            Console.WriteLine(AppSettings.MailjetReceiverEmail);

            MailjetClient client = new MailjetClient(
                AppSettings.MailjetPublicKey, 
                AppSettings.MailjetPrivateKey
                );


            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
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

                 {"Subject", $"New Applicant for {jobTitle} role"},
                 {"TextPart", $"There is a new applicant for the {jobTitle} role. This applicant has applied through your website. Here are the details: \n\n Name: {jobApplicant.FirstName} {jobApplicant.LastName} \n\n Email: {jobApplicant.Email} \n\n Mobile Number: {jobApplicant.MobileNumber} \n\n About Applicant: {jobApplicant.About} \n\n\n"},
                 {"InlinedAttachments", new JArray {
                  new JObject {
                   {"ContentType", "application/pdf"},
                   {"Filename", $"Resume - {jobApplicant.FirstName} {jobApplicant.LastName}"},
                   {"Base64Content", $"{Encoder.ConvertToBase64(resumeUpload)}"}
                   }
                  }}
                 }
                });


            MailjetResponse response = await client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.GetErrorMessage());

            request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
            .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", AppSettings.MailjetSystemEmail},
                  {"Name", "System Email"}
                  }},

                 {"To", new JArray {
                  new JObject {
                   {"Email", jobApplicant.Email},
                   }
                  }},

                 {"Subject", $"{jobTitle} role - Job Application successfully sent."},
                 {"TextPart", $"Your application for the role {jobTitle} has been sent. We will get back to you with our decision very soon. In the meantime, we would like you to be patient. \n\n Summary \n\n Name: {jobApplicant.FirstName} {jobApplicant.LastName} \n\n Email: {jobApplicant.Email} \n\n Mobile Number: {jobApplicant.MobileNumber} \n\n About Applicant: {jobApplicant.About} \n\n\n"},
                 {"InlinedAttachments", new JArray {
                  new JObject {
                   {"ContentType", "application/pdf"},
                   {"Filename", $"Resume - {jobApplicant.FirstName} {jobApplicant.LastName}"},
                   {"Base64Content", $"{Encoder.ConvertToBase64(resumeUpload)}"}
                   }
                  }}
                 }
                });


            response = await client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.GetErrorMessage());
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _jobListingRepo.DeleteAsync(id);
        }

        public IEnumerable<Job>? GetAll()
        {
            return _jobListingRepo.GetAll();
        }

        public IEnumerable<Job>? GetAllActive()
        {
            return _jobListingRepo.GetAllActive();
        }

        public Job? GetById(int id)
        {
            return _jobListingRepo.GetByCondition(j => j.JobID == id);
        }

        public Job? GetByName(string jobName)
        {
            return _jobListingRepo.GetByCondition(j => j.JobTitle == jobName);
        }

        public async Task<bool> UpdateAsync(Job updatedObj)
        {
            return await _jobListingRepo.UpdateAsync(updatedObj);
        }
    }
}