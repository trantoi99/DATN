using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Mailjet.Client.TransactionalEmails;
using Electric.Models;

namespace Electric.EmailService
{
    public class ConfigEmail
    {
        public async Task RunAsync(string emailUser, string nameUser)
        {
            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("3d1d626d435f0cf99d83511454aab891"), Environment.GetEnvironmentVariable("8a2a4a39d1364e0343c703a7bc886d19"))
            {
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "toi171201330@st.utc.edu.vn"},
        {"Name", "Website TT"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          emailUser
         }, {
          "Name",
          nameUser
         }
        }
       }
      }, {
       "Subject",
       "Greetings from Mailjet."
      }, {
       "TextPart",
       "My first Mailjet email"
      }, {
       "HTMLPart",
       "<h3>Dear passenger 1, welcome to <a href='https://www.mailjet.com/'>Mailjet</a>!</h3><br />May the delivery force be with you!"
      }, {
       "CustomID",
       "AppGettingStartedTest"
      }
     }
             });
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                
            }
            else
            {

            }
        }
    }
}
