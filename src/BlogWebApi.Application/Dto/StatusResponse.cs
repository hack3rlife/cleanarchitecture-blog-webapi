using System;

namespace BlogWebApi.Application.Dto
{
    public class StatusResponse
    {
        public DateTime Started { get; set; }
        public string Server { get; set; }
        public string OsVersion { get; set; }
        public string AssemblyVersion { get; set; }
    }
}
