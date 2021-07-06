﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Electric.EmailService.Response
{
    public class IdentityMessage
    {
        public string Destination { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailAddress { get; set; }
        public string NameObject { get; set; }
    }
}
