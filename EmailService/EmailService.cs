using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Mailjet.Client.TransactionalEmails;
using Electric.Models;
using Electric.EmailService.Response;

namespace Electric.EmailService
{
    public static class EmailService
    {
        public static async Task<ResponseEmail> SendAsync(IdentityMessage message)
        {
            try
            {
                MailjetClient mailjet = new MailjetClient(
                    Environment.GetEnvironmentVariable(AppSetting.Configuration.EmailConfig.ApiKey),
                    Environment.GetEnvironmentVariable(AppSetting.Configuration.EmailConfig.ApiSecret));

                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource
                }.Property(Send.FromEmail, AppSetting.Configuration.EmailConfig.EmailIdentifier)
                .Property(Send.FromName, AppSetting.Configuration.EmailConfig.NameIdentifier)
                .Property(Send.Subject, message.Subject)
                .Property(Send.TextPart, message.Destination)
                .Property(Send.HtmlPart, message.Body)
                .Property(Send.Recipients, new JArray
                {
                    new JObject
                    {
                        {"Email", message.EmailAddress },
                        {"Name", message.NameObject }
                    }
                });

                MailjetResponse response = await mailjet.PostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new ResponseEmail { Successed = true, Code = 200, ErrorInfo = "", ErrorMessage = "" };
                }

                return new ResponseEmail
                {
                    Successed = false,
                    Code = response.StatusCode,
                    ErrorMessage = response.GetErrorMessage(),
                    ErrorInfo = response.GetErrorInfo()
                };

            }
            catch (Exception ex)
            {
                return new ResponseEmail { Successed = false, Code = 500, ErrorMessage = ex.Message, ErrorInfo = ex.Message };
            }
        }
    }
}
