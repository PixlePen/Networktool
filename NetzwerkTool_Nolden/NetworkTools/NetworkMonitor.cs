using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;

class NetworkMonitor
{
    // Methode zur Überwachung der Verbindung
    public static void StartMonitoring()
    {
        Console.Write("Geben Sie die zu überwachende IP-Adresse ein: ");
        string ip = Console.ReadLine();
        string logFilePath = "network_log.txt";

        // Überwache die Verbindung alle 60 Sekunden
        while (true)
        {
            string status = "Offline";
            Ping ping = new Ping();
            PingReply reply = ping.Send(ip);
            if (reply.Status == IPStatus.Success)
            {
                status = "Online";
            }

            // Logge das Ergebnis
            File.AppendAllText(logFilePath, $"{DateTime.Now} - {ip} ist {status}\n");

            Console.WriteLine($"IP {ip} ist {status}. Loggen...");
            Thread.Sleep(60000);  // 60 Sekunden warten
        }
    }
}
