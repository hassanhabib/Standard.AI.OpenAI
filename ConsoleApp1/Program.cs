using System.ComponentModel;
using OpenAI.NET.Models.Completions;

var inputCompletion = new Completion
{
    Request = new CompletionRequest
    {
        Prompt = new[] { "hello" },
        Model = "text-davinci-003"
    }
};

// using builder with a string
var inputCompletionUsingBuilder1 = new CompletionBuilder()
    .WithRequest(requestBuilder => requestBuilder
        .WithPrompt("hello")
        .WithModel("my model")
        .Build()
    )
    .Build();


// using builder with a struct
var inputCompletionUsingBuilder2 = new CompletionBuilder()
    .WithRequest(requestBuilder => requestBuilder
        .WithPrompt("hello")
        .WithModel(Model.TextDavinci003)
        .Build()
    )
    .Build();

// using builder with a enum
var inputCompletionUsingBuilder3 = new CompletionBuilder()
    .WithRequest(requestBuilder => requestBuilder
        .WithPrompt("hello")
        .WithModel(ModelAsEnum.TextDavinci003)
        .Build()
    )
    .Build();

int x = 0;

public struct Model
{
    public static readonly string TextDavinci003 = "text-davinci-003";
}

public enum ModelAsEnum
{
    [Description("text-davinci-003")]
    TextDavinci003
}