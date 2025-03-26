using BookEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

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

    // Build the kernel
    Kernel kernel = builder.Build();

    var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

    // Add a persona
    var persona = "You are an event booking agent for events in Nairobi.";

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
        // var executionSettings = new OpenAIPromptExecutionSettings()
        // {
        //     FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        // };

        // Get the response from the AI
        var result = await chatCompletionService.GetChatMessageContentAsync(
            history,
            // executionSettings: executionSettings,
            kernel: kernel
        );

        // Print the results
        Console.WriteLine("Assistant > " + result);

        // Add the message from the agent to the chat history
        history.AddMessage(result.Role, result.Content ?? string.Empty);
    } while (userInput is not null);
}
