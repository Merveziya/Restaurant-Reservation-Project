#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0060

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.OpenApi;
using System;
using System.Threading.Tasks;


// Create the kernel with Azure OpenAI
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    "gpt-35-turbo",
    "model",
    "API key");

// Add the BookingsPlugin
builder.Plugins.AddFromType<BookingsPlugin>();

Kernel kernel = builder.Build();

string openApiFilePath = Path.Combine(Directory.GetCurrentDirectory(), "openapi.json");

#pragma warning disable SKEXP0040
kernel.ImportPluginFromOpenApiAsync(
    pluginName: "TimeZone",
    new Uri("https://worldtimeapi.org/api")
);
#pragma warning disable SKEXP0040



// Retrieve the chat completion service
IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Enable automatic function calling
OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

// Create the chat history - Persona
ChatHistory history = new ChatHistory("""
            You are a friendly assistant who helps users locate a restaurant of a specific 
            cuisine in their location and book a table. 
            If the user provides incomplete information, ask more questions to fill in the details.
            """);

List<string> messageHistory = new List<string>();

string? userInput;

do
{
    //Get input from the user
    Console.Write("User > ");
    userInput = Console.ReadLine();

    //  Add user message to chat history
    history.AddUserMessage(userInput);

    // Add user entry to chat history
    messageHistory.Add(userInput);

    // Get response from AI and call automatic function calling
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Print results
    Console.WriteLine("Assistant > " + result);

    // Add assistant's message to chat history
    history.AddMessage(result.Role, result.Content ?? string.Empty);

} while (!string.IsNullOrEmpty(userInput));

