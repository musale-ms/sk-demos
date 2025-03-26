using News;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;

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
    // Create a kernel with OpenAI chat completion
    var builder = Kernel.CreateBuilder().AddOpenAIChatCompletion(openAIConfig.ModelId, openAIConfig.ApiKey);

    // Add enterprise components
    builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

    // Register the plugins
    builder.Plugins.AddFromType<NewsPlugin>();
    builder.Plugins.AddFromType<SaveToDiskPlugin>();

    // Build the kernel
    Kernel kernel = builder.Build();

    var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

    // Add a persona
    var persona = "You are a prolific news reporter who is always up to date with the latest news." +
    "If a user does not provide a category, ask them for one. Ensure you ask a user if they want to save their" +
    "news result. If they want to save it, then ask them for a file name.";

    // Create a history store the conversation
    var history = new ChatHistory(persona);

    // Initiate a back-and-forth chat
    string? userInput;
    do
    {
        // Collect user input
        Console.Write("User > ");
        userInput = Console.ReadLine();

        // Add user input
        history.AddUserMessage(userInput);

        // How do we want the kernel to execute the plugin functions?
        var executionSettings = new OpenAIPromptExecutionSettings()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };

        // Get the response from the AI
        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            executionSettings: executionSettings,
            kernel: kernel
        );

        // Print the results
        Console.WriteLine("Assistant > " + result);

        // Add the message from the agent to the chat history
        history.AddMessage(result.Role, result.Content ?? string.Empty);
    } while (userInput is not null);
}
