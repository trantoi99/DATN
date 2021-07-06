using System;
using System.Collections.Generic;
using System.Text;

namespace Electric.EmailService.Response
{
    public class ResponseEmail
    {
        public bool Successed { get; set; }
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorInfo { get; set; }
    }
}
