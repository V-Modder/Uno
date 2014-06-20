using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnoSrv
{
    class Program
    {
        static void Main(string[] args)
        {
            int playercount = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-player")
                    playercount = Convert.ToInt32(args[i + 1]);
            }
            if (playercount != 0)
            {
                UnoSrv server = new UnoSrv(playercount);
                server.Start();
            }
        }
    }
}
