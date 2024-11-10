using System;
using System.Net.Sockets;

class PortScanner
{
    // Methode zum Testen eines Ports auf einer IP
    public static bool TestPort(string ip, int port)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ip, port);  // Versuche die Verbindung
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    // Methode zum Scannen mehrerer Ports
    public static void ScanPorts()
    {
        Console.Write("Geben Sie die IP-Adresse ein: ");
        string ip = Console.ReadLine();

        int[] ports = { 80, 443, 22, 21, 8080 }; // Beispiel-Ports
        foreach (var port in ports)
        {
            Console.WriteLine($"Scanne Port {port}...");
            if (TestPort(ip, port))
            {
                Console.WriteLine($"Port {port} auf {ip} ist offen.");
            }
            else
            {
                Console.WriteLine($"Port {port} auf {ip} ist geschlossen.");
            }
        }
    }
}
