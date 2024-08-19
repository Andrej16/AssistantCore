using Azure;
// Add Azure OpenAI package
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace AssistantCore.AI._02_azure_openai_api;

public class IntegrateAzureOpenAI
{
    public int Run()
    {
        // Build a config object and retrieve user settings.
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        string? oaiEndpoint = config["AzureOAIEndpoint"];
        string? oaiKey = config["AzureOAIKey"];
        string? oaiDeploymentName = config["AzureOAIDeploymentName"];

        if (string.IsNullOrEmpty(oaiEndpoint) 
            || string.IsNullOrEmpty(oaiKey) 
            || string.IsNullOrEmpty(oaiDeploymentName))
        {
            Console.WriteLine("Please check your appsettings.json file for missing or incorrect values.");
            return 1;
        }

        // Initialize the Azure OpenAI client
        Uri endpoint = new Uri(oaiEndpoint);
        AzureKeyCredential keyCredential = new AzureKeyCredential(oaiKey);
        OpenAIClient client = new OpenAIClient(endpoint, keyCredential);

        // System message to provide context to the model
        string systemMessage = "I am a hiking enthusiast named Forest who helps people " +
            "discover hikes in their area. If no area is specified, " +
            "I will default to near Rainier National Park. " +
            "I will then provide three suggestions for nearby hikes that vary in length. " +
            "I will also share an interesting fact about the local nature on the hikes " +
            "when making a recommendation.";

        // Initialize messages list
        var messagesList = new List<ChatRequestMessage>()
        {
            new ChatRequestSystemMessage(systemMessage),
        };

        do
        {
            Console.WriteLine("Enter your prompt text (or type 'quit' to exit): ");
            string? inputText = Console.ReadLine();
            if (inputText == "quit") 
                break;

            // Generate summary from Azure OpenAI
            if (inputText == null)
            {
                Console.WriteLine("Please enter a prompt.");
                continue;
            }

            Console.WriteLine("\nSending request for summary to Azure OpenAI endpoint...\n\n");

            // Add code to send request...
            // Build completion options object
            ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatRequestSystemMessage(systemMessage),
                    new ChatRequestUserMessage(inputText),
                },
                MaxTokens = 400,
                Temperature = 0.7f,
                DeploymentName = oaiDeploymentName
            };

            // Add messages to the completion options
            foreach (ChatRequestMessage chatMessage in messagesList)
            {
                chatCompletionsOptions.Messages.Add(chatMessage);
            }

            // Send request to Azure OpenAI model
            ChatCompletions response = client.GetChatCompletions(chatCompletionsOptions);

            // Print the response
            string completion = response.Choices[0].Message.Content;

            // Add generated text to messages list
            messagesList.Add(new ChatRequestAssistantMessage(completion));

            Console.WriteLine("Response: " + completion + "\n");
        } while (true);

        return 1;
    }
}
