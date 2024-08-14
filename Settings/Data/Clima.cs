using Godot;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

public partial class ClimaAPI : Node
{
    private const string API_KEY = "cc652f107ca636e5ed854610f5487812";
    private const string BASE_URL = "http://api.openweathermap.org/data/2.5/weather";

    //3836873 es el código de Tucumán según la lista de la web. Se solicita
    //San Miguel de Tucumán fifura

    private static readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

    public static async Task<string> GetClima(string city)
    {
        string url = $"{BASE_URL}?q={city}&appid={API_KEY}&units=metric";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseBody);
            string clima = doc.RootElement.GetProperty("weather")[0].GetProperty("main").GetString();

            return clima;
        }
        catch (HttpRequestException e)
        {
            GD.PrintErr($"Error: {e.Message}");
            return null;
        }
    }
}