using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class server
{
  public static void Main(String[] args)
  {
        //String connection = @"";
        String connection = args[0];
        if (args.Length != 1){
          Console.WriteLine("Connection string required!");
          System.Environment.Exit(-1);
        }
      tcpserver tcps = new tcpserver("127.0.0.1",13000,connection);
      tcps.Start();
      tcps.Run();
  }
}
