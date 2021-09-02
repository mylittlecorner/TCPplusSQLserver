using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Data.SqlClient;

class tcpserver
{
    private Byte[] _bytes;
    private String _data;
    private TcpClient _client;
    private NetworkStream _stream;
    private readonly TcpListener _server;
    private readonly Int32 _port;
    private readonly IPAddress _localAddr;
    private readonly SQLConn _sqlconn;

        public tcpserver(String host, Int32 port, String sqlConnStr)
        {
          _port = port;
          _localAddr = IPAddress.Parse(host);
          _server = new TcpListener(_localAddr, port);
          _bytes = new Byte[256];
          _data = null;
          _sqlconn = new SQLConn(sqlConnStr);
        }

        public void Start()
        {
          _server.Start();
        }


        public void Run()
        {
          try{
                  byte[] msg = System.Text.Encoding.ASCII.GetBytes("OK");
                  while(true)
                  {
                    Console.Write("Waiting for a connection... ");

                    _client = _server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    _data = null;

                    _stream = _client.GetStream();

                    int i;

                    while((i = _stream.Read(_bytes, 0, _bytes.Length))!=0)
                    {
                          _data = System.Text.Encoding.ASCII.GetString(_bytes, 0, i);
                          Console.WriteLine("Received: {0}", _data);
                          _sqlconn.DbOpen();
                          _sqlconn.InsertValues(_data);
                          _sqlconn.DbClose();
                          _stream.Write(msg, 0, msg.Length);
                          Console.WriteLine("Sent: {0}", _data);
                    }

                    _client.Close();
                  }
          }
          catch(SocketException e)
          {
            Console.WriteLine("SocketException: {0}", e);
          }
          finally
          {
             _server.Stop();
          }
          Console.WriteLine("\nHit enter to continue...");
          Console.Read();
        }
}
