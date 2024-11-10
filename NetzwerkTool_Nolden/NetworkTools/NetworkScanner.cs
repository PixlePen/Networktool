using System;
using System.Net.NetworkInformation;

class NetworkScanner
{
    // Methode zum Testen der Verbindung
    public static bool TestConnection(string ip)
    {
        try
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(ip, 1000); // Timeout: 1 Sekunde
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }

    // Methode zum Scannen des Netzwerks
    public static void ScanNetwork()
    {
        Console.Write("Geben Sie das Subnetz ein (z.B. 192.168.1): ");
        string subnet = Console.ReadLine();

        for (int i = 1; i <= 254; i++)  // IP-Adressen von 1 bis 254
        {
            string ip = $"{subnet}.{i}";
            Console.WriteLine($"Teste IP: {ip}");
            if (TestConnection(ip))
            {
                Console.WriteLine($"IP {ip} ist erreichbar.");
            }
            else
            {
                Console.WriteLine($"IP {ip} ist nicht erreichbar.");
            }
        }
    }
}
