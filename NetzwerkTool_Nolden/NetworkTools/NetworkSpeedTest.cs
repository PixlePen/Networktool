using System;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;

class NetworkSpeedTest
{
    // URL für den Speedtest-Download-Server (kann durch jeden anderen geeigneten Server ersetzt werden)
    static readonly string downloadUrl = "https://speed.hetzner.de/100MB.bin";  // Eine große Datei von einem schnellen Server

    // Methode zur Messung der Download-Geschwindigkeit
    public static async Task<double> MeasureDownloadSpeed(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Lade die Datei herunter (simuliert den Download-Test)
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var contentLength = response.Content.Headers.ContentLength;
            if (contentLength == null)
            {
                throw new Exception("Keine Datei-Größe gefunden.");
            }

            long totalBytes = 0;
            var buffer = new byte[8192];  // Buffer für die Datenübertragung
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    totalBytes += bytesRead;
                }
            }

            stopwatch.Stop();

            double downloadSpeedMbps = (totalBytes * 8) / (stopwatch.ElapsedMilliseconds * 1024.0);  // in Megabit pro Sekunde
            return downloadSpeedMbps;
        }
    }

    // Methode zur Messung der Upload-Geschwindigkeit (simuliert durch das Hochladen einer Datei)
    public static async Task<double> MeasureUploadSpeed(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Simuliere den Upload, indem wir eine Dummy-Datei hochladen
            byte[] data = new byte[10000000];  // 10 MB Dummy-Daten
            var content = new ByteArrayContent(data);
            content.Headers.Add("Content-Type", "application/octet-stream");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            stopwatch.Stop();

            double uploadSpeedMbps = (data.Length * 8) / (stopwatch.ElapsedMilliseconds * 1024.0);  // in Megabit pro Sekunde
            return uploadSpeedMbps;
        }
    }

    // Methode zur Messung der Latenz (Ping)
    public static async Task<double> MeasurePingLatency(string host)
    {
        using (var client = new System.Net.NetworkInformation.Ping())
        {
            var reply = await client.SendPingAsync(host);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                return reply.RoundtripTime;
            }
            else
            {
                throw new Exception("Ping fehlgeschlagen.");
            }
        }
    }

    // Die Hauptmethode, um den Test durchzuführen
    public static async Task RunSpeedTest()
    {
        try
        {
            // 1. Latenz messen
            Console.WriteLine("Latenz messen...");
            double pingLatency = await MeasurePingLatency("google.com"); // Du kannst auch andere Server verwenden
            Console.WriteLine($"Latenz: {pingLatency} ms");

            // 2. Download-Geschwindigkeit messen
            Console.WriteLine("Download-Geschwindigkeit messen...");
            double downloadSpeed = await MeasureDownloadSpeed(downloadUrl);
            Console.WriteLine($"Download-Geschwindigkeit: {downloadSpeed:F2} Mbps");

            // 3. Upload-Geschwindigkeit messen
            Console.WriteLine("Upload-Geschwindigkeit messen...");
            double uploadSpeed = await MeasureUploadSpeed("https://httpbin.org/post"); // Dummy-URL für Upload
            Console.WriteLine($"Upload-Geschwindigkeit: {uploadSpeed:F2} Mbps");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }
}
