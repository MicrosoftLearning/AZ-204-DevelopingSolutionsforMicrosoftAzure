if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
{
    Console.WriteLine("Current IP Addresses:");

    // Get host entry for current hostname
    string hostname = System.Net.Dns.GetHostName();
    System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(hostname);
    
    // Iterate over each IP address and render their values
    foreach(System.Net.IPAddress address in host.AddressList)
    {
        Console.WriteLine($"\t{address}");
    }
}
else
{
    Console.WriteLine("No Network Connection");
}
