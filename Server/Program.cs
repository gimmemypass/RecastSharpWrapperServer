using Cmd.Terminal;
using Server.Debugger;

namespace Server
{
    public class Program
    {
        private static Server server;
        private const int millisecondsTimeout = 100;

        public static void Main(string[] args)
        {
            RecastDebug.Init();
            server = new Server();
            server.Start();
        
            new Thread(GameLoop).Start(); 
            Terminal.RunCommand("debug -se");
            Terminal.Listen();
        }

        private static void GameLoop()
        {
            while (true)
            {
                server.Update();
                Thread.Sleep(millisecondsTimeout);
            }
        }
    }
}