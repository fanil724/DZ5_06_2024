using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/Index", () => "Index page");
app.Map("/time", () => $"Текущее время: {DateTime.Now.ToLongTimeString}");
app.Map("/date", () => $"Текущее дата: {DateTime.Now.ToLongDateString}");

app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
{
    var sb = new StringBuilder();
    var endpoints = endpointSources.SelectMany(es => es.Endpoints);
    foreach (var endpoint in endpoints)
    {
        sb.AppendLine(endpoint.DisplayName);
        sb.AppendLine(string.Join("-", endpoint.Metadata.Select(x => x.ToString())));
        sb.AppendLine(endpoint.RequestDelegate?.Target?.ToString());
        if (endpoint is RouteEndpoint routeEndpoint)
        {
            sb.AppendLine(routeEndpoint.RoutePattern.PathSegments.ToString());
            sb.AppendLine(routeEndpoint.RoutePattern.RawText);
            sb.AppendLine(routeEndpoint.RoutePattern.InboundPrecedence.ToString());
            sb.AppendLine(routeEndpoint.RoutePattern.OutboundPrecedence.ToString());
            sb.AppendLine( routeEndpoint.RoutePattern.RequiredValues.Values.ToString());
            sb.AppendLine( routeEndpoint.RoutePattern.ParameterPolicies.Values.ToString());
            sb.AppendLine( routeEndpoint.RoutePattern.Parameters.ToString());
        }
        sb.AppendLine();
    }
    return sb.ToString();
});

app.MapGet("/", () => "Hello World!");

app.Run();
