using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class server
{
  public static void Main(String[] args)
  {
     if (args.Length != 3){
          Console.WriteLine("Invalid command line arguments!");
          System.Environment.Exit(-1);
        }
        //String connection = @"";
		  String host = args[0];
		  String port = args[1];
      String connection = args[2];
        
      tcpserver tcps = new tcpserver(host,Convert.ToInt32(port),connection);
      tcps.Start();
      tcps.Run();
  }
}
