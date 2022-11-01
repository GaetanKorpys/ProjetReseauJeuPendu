using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A C# program for Client
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientJeuPendu
{

	class Client
	{

		private string _ip;
		private int _port;

		private static Client _instanceUnique = null; //Implémentation du DP Singleton

		private Client(string ip, int port)
        {
			_ip = ip;
			_port = port;
        }

		private Client()
        {
			_ip = "127.0.0.1";
			_port = 8091;
        }

		public static Client getInstance()
		{
			if (_instanceUnique == null)
			{
				_instanceUnique = new Client();
			}

			return _instanceUnique;
		}

		// ExecuteClient() Method
		public void ExecuteClient()
		{

			try
			{

				// Establish the remote endpoint
				// for the socket. This example
				// uses port 11111 on the local
				// computer.
				IPHostEntry ipHost = Dns.GetHostEntry(_ip);
				IPAddress ipAddr = ipHost.AddressList[0];
				IPEndPoint localEndPoint = new IPEndPoint(ipAddr, _port);

				// Creation TCP/IP Socket using
				// Socket Class Constructor
				Socket sender = new Socket(ipAddr.AddressFamily,
						SocketType.Stream, ProtocolType.Tcp);

				try
				{

					// Connect Socket to the remote
					// endpoint using method Connect()
					sender.Connect(localEndPoint);

					string message = "Connexion avec le serveur réussie.\n";
					message += "-- Adresse IP du serveur : " + _ip + "\n";
					message += "-- Port du serveur : " + _port + "\n";
					message += "\n\n";
					message += "Que souhaitez-vous faire ?\n";
					message += "Utiliser la commande !HELP pour afficher l'ensemble des commandes disponibles.";

					// We print EndPoint information
					// that we are connected
					Console.WriteLine(message);

					// Creation of message that
					// we will send to Server
					
					byte[] messageSent = Encoding.ASCII.GetBytes(message);
					int byteSent = sender.Send(messageSent);

					// Data buffer
					byte[] messageReceived = new byte[1024];

					// We receive the message using
					// the method Receive(). This
					// method returns number of bytes
					// received, that we'll use to
					// convert them to string
					int byteRecv = sender.Receive(messageReceived);
					Console.WriteLine("Message from Server -> {0}",
						Encoding.ASCII.GetString(messageReceived,
													0, byteRecv));

					// Close Socket using
					// the method Close()
					sender.Shutdown(SocketShutdown.Both);
					sender.Close();
				}

				// Manage of Socket's Exceptions
				catch (ArgumentNullException ane)
				{

					Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
				}

				catch (SocketException se)
				{

					Console.WriteLine("SocketException : {0}", se.ToString());
				}

				catch (Exception e)
				{
					Console.WriteLine("Unexpected exception : {0}", e.ToString());
				}
			}

			catch (Exception e)
			{

				Console.WriteLine(e.ToString());
			}
		}
	}
}

