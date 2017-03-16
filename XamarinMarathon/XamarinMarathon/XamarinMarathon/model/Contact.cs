using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinMarathon.model
{
    [DataTable("Contatos")]
    public class Contact
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Nome")]
        public string Name { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
