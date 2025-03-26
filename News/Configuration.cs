namespace News;

public sealed class LLMConfigs
{
    public required LLMConfig AzureOpenAI { get; set; } = null!;
    public required LLMConfig OpenAI { get; set; } = null!;
}

public sealed class LLMConfig
{
    public required string ModelId { get; set; } = null!;
    public required string Endpoint { get; set; } = null!;
    public required string ApiKey { get; set; } = null!;
}
