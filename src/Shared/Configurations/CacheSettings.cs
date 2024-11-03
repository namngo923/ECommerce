using SPSVN.Shared.Constants;

namespace SPSVN.Shared.Configurations;

public class CacheSettings
{
    public const string SectionName = "CacheSettings";

    public string ConnectionString { get; set; } = string.Empty;
    public string Type { get; set; } = CacheType.Memory;
}