using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            Console.WriteLine("Netzwerk- und Sicherheits-Tool");
            Console.WriteLine("1. Netzwerkscanner");
            Console.WriteLine("2. Portscanner");
            Console.WriteLine("3. Netzwerküberwachung");
            Console.WriteLine("4. VPN-Check");
            Console.WriteLine("5. Netzwerkgeschwindigkeitstest");
            Console.WriteLine("6. Beenden");
            Console.Write("Wählen Sie eine Option (1-6): ");
            
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    NetworkScanner.ScanNetwork();
                    break;
                case "2":
                    PortScanner.ScanPorts();
                    break;
                case "3":
                    NetworkMonitor.StartMonitoring();
                    break;
                case "4":
                    VPNChecker.CheckIP();
                    break;
                case "5":
                    // Aufruf des Netzwerkgeschwindigkeitstests
                    await NetworkSpeedTest.RunSpeedTest();
                    break;
                case "6":
                    run = false;
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe. Bitte erneut versuchen.");
                    break;
            }

            if (run)
            {
                Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                Console.ReadKey();
            }
        }
    }
}
