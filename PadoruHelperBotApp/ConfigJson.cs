using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadoruHelperBotApp
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefixes")]
        public string[] Prefixes { get; private set; }
    }
}
