using System;
using System.Net;
using System.Net.Sockets;

namespace KartRider
{
	public class RouterListener
	{
		public static string sIP;

		public static int port;

		public static string forceConnect;

		public static System.Net.IPEndPoint CurrentUDPServer { get; set; }

		public static string ForceConnect { get; set; }

		public static TcpListener Listener { get; private set; }

		public static SessionGroup MySession { get; set; }

		static RouterListener()
		{
			string str = "127.0.0.1";
			RouterListener.sIP = str;
			int str1 = 39312;
			RouterListener.port = str1;
		}

		public static void OnAcceptSocket(IAsyncResult ar)
		{
			try
			{
				Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				Socket clientSocket = RouterListener.Listener.EndAcceptSocket(ar);
				RouterListener.forceConnect = RouterListener.sIP;
				if ((RouterListener.ForceConnect == "" ? false : RouterListener.ForceConnect != "0.0.0.0"))
				{
					RouterListener.forceConnect = RouterListener.ForceConnect;
				}
				RouterListener.MySession = new SessionGroup(clientSocket, null);
				GameSupport.PcFirstMessage();
			}
			catch
			{
			}
			RouterListener.Listener.BeginAcceptSocket(new AsyncCallback(RouterListener.OnAcceptSocket), null);
		}

		public static void Start()
		{
			Console.WriteLine("Load server IP : {0}:{1}", (object)RouterListener.sIP, (object)RouterListener.port);
			RouterListener.ForceConnect = "";
			RouterListener.Listener = new TcpListener(IPAddress.Parse(RouterListener.sIP), RouterListener.port);
			RouterListener.Listener.Start();
			RouterListener.Listener.BeginAcceptSocket(new AsyncCallback(RouterListener.OnAcceptSocket), (object)null);
			RouterListener.CurrentUDPServer = new System.Net.IPEndPoint(IPAddress.Parse(RouterListener.sIP), 39311);
		}
	}
}