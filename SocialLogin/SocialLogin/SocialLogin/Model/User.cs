using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialLogin.Model
{
    public class User
    {

        [JsonProperty("userId")]
        public String UserId { get; set; }
    }
}
