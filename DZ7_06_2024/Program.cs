using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/avto/{marka:alpha:length(2,50)}/{type:regex(\\w*):length(2,50)}/{cena:decimal:range(1,60000000)}",
    (string marka,string type, double cena) => $"Marka avto: {marka}, Type avto: {type}, Price avto: {cena}");

app.Map("/mobile/{marka:alpha:length(2,50)}/{model:regex(\\w*):length(2,50)}/{date:regex(\\d{{2}}-\\d{{2}}-\\d{{4}})}",
    (string marka, string model, string date) => $"Marla phone: {marka}, Mdoel phone: {model}, Release date: {date}");


app.MapGet("/", () => "Hello World!");

app.Run();
