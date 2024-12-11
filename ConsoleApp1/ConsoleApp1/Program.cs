using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class API
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("https://opentdb.com/api.php?amount=20&category=9&difficulty=easy&type=multiple");
            string apiUrl = Console.ReadLine();

            // API uitlezen
            await ReadApi(apiUrl);
        }

        static async Task ReadApi(string apiUrl)
        {
            // HttpClient instellen
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Bezig met het ophalen van data...");

                    // API-aanroep uitvoeren
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Controleren of de aanroep succesvol is
                    if (response.IsSuccessStatusCode)
                    {
                        // Data als string uitlezen
                        string data = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Ontvangen data:");
                        Console.WriteLine(data);
                    }
                    else
                    {
                        Console.WriteLine($"Fout bij het ophalen van data: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Er is een fout opgetreden: {ex.Message}");
                }
            }
        }
    }
}
