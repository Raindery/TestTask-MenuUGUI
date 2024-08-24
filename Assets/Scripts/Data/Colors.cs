using System.Collections.Generic;
using Newtonsoft.Json;


public struct Colors
{
    [JsonProperty("colors")]
    public List<Color> ColorArray { get; set; }
}

[System.Serializable]
public struct Color
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("color")]
    public string ColorValue { get; set; }
}
