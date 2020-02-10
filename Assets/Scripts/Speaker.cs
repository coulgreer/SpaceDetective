using Newtonsoft.Json;

public class Speaker {
    public string Name { get; }
    public string PortraitPath { get; }

    [JsonConstructor]
    public Speaker(string name, string portraitPath) {
        Name = name;
        PortraitPath = portraitPath;
    }

    public override bool Equals(System.Object obj) {
        if (obj == null) { return false; }

        Speaker speaker = obj as Speaker;
        if (speaker == null) {
            return false;
        } else {
            return this.Name.Equals(speaker.Name, System.StringComparison.InvariantCultureIgnoreCase)
                && this.PortraitPath.Equals(speaker.PortraitPath);
        }
    }

    public override int GetHashCode() {
        return Name.GetHashCode() ^ PortraitPath.GetHashCode();
    }
}
