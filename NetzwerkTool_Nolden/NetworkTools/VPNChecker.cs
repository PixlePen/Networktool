using System;
using System.Net.Http;
using System.Threading.Tasks;

class VPNChecker
{
    // Methode zum Abrufen der IP-Informationen
    public static async Task CheckIP()
    {
        Console.Write("Geben Sie eine IP-Adresse ein, um den VPN-Status zu überprüfen: ");
        string ip = Console.ReadLine();
        string url = $"https://ipinfo.io/{ip}/json";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string response = await client.GetStringAsync(url);
                Console.WriteLine("IP-Informationen: " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler bei der Anfrage: {ex.Message}");
            }
        }
    }
}
