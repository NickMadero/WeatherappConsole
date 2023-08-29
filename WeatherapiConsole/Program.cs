using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Boolean finished = true;
        while (finished != false)
        {
            Console.WriteLine("Enter city:");
        string city = Console.ReadLine();

        String apiKey = "b3d734cf5e18adcd52b4c7cebdce3ee5";
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=imperial";

       
       
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(responseBody);

                    var temperature = jsonDocument.RootElement.GetProperty("main").GetProperty("temp").GetDouble();
                    var weatherDescription = jsonDocument.RootElement.GetProperty("weather")[0].GetProperty("description").GetString();
                    var humidity = jsonDocument.RootElement.GetProperty("main").GetProperty("humidity").GetDouble();

                    Console.WriteLine($"Temperature: {temperature}");
                    Console.WriteLine($"Weather: {weatherDescription}");
                    Console.WriteLine($"Humidity: {humidity}");
                }
                else
                {
                    Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                }
            
                
            }
            Console.WriteLine("would you like to view another city");
            string userinput = Console.ReadLine();
            if (userinput.Equals("yes"))
            {
                finished = true;

            } else
            {
                finished= false;
            }
        }
    }
}
