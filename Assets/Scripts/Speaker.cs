using Newtonsoft.Json;

public class Speaker {
    public string Name { get; }
    public string PortraitPath { get; }

    [JsonConstructor]
    public Speaker(string name, string portraitPath) {
        Name = name;
        PortraitPath = portraitPath;
    }
}
