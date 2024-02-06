﻿namespace AzureFunctions.Configurations
{
    public class EmailServiceConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Origin { get; set; }
    }
}
