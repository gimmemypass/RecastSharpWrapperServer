using Cmd.Terminal;
using LiteNetLib;
using LiteNetLib.Utils;
using Server.Debugger;

namespace Server; 

public class Program
{
    private static Server server;

    public static void Main(string[] args)
    {
        Debug.Init();
        server = new Server();
        server.Start();
        
        // Terminal.RunCommand("debug -se");
        // Terminal.Listen();
        
        while (true)
        {
            server.Update();
            Thread.Sleep(15);
        }
    }
}