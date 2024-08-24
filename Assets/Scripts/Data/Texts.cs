using System.Collections.Generic;
using Newtonsoft.Json;

public struct Texts
{
    [JsonProperty("texts")]
    public List<string> TextArray { get; set; }
}