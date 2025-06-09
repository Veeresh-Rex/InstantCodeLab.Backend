using System.Text.Json.Serialization;

namespace InstantCodeLab.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LanguageCode
{
    java,
    bash,
    c,
    csharp,
    cpp17,
    dart,
    go,
    kotlin,
    nodejs,
    python3,
    ruby,
    rust,
    sql,
    typescript,
}
