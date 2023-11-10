using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizAPI;var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var text = File.ReadAllText("questions.json");
var questions = JsonConvert.DeserializeObject<List<Question>>(text);

app.MapGet("/{count}", (HttpRequest request) =>
{
    var count = int.Parse(request.RouteValues["count"]!.ToString());
    var res = Quiz(count);
    return new OkObjectResult(res);
});

app.Run();



List<Question> Quiz(int questionCount)
{
    var res = new List<Question>();
    for (var i = 0; i < questionCount; i++)
    {
        var r = new Random();
        if (questions is null)
            throw new Exception("Brak pytań");
        var q = r.Next(0, questions.Count - 1);
        res.Add(questions[q]);
    }
    return res;
}