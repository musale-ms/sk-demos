using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using News;

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.development.json")
    .AddEnvironmentVariables()
    .Build();

// using OpenAI
LLMConfigs? llms = config.GetRequiredSection("LLMConfigs").Get<LLMConfigs>();
LLMConfig? openAIConfig = null;

if (llms?.OpenAI is not null)
{
    openAIConfig = llms.OpenAI;
}

if (openAIConfig is not null)
{
    Console.WriteLine(openAIConfig.ModelId);
}