﻿using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTickerBot.Extensions
{
	public static class ClientWebSocketExtensions
	{
		public static async Task SendStringAsync ( this ClientWebSocket ws, string str )
		{
			var bytesToSend = new ArraySegment<byte> ( Encoding.UTF8.GetBytes ( str ) );
			await ws.SendAsync ( bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None );
		}

		public static async Task<string> ReceiveStringAsync ( this ClientWebSocket ws, int bufferSize = 10240 )
		{
			var bytesReceived = new ArraySegment<byte> ( new byte[bufferSize] );
			var result = await ws.ReceiveAsync ( bytesReceived, CancellationToken.None );
			Console.WriteLine ( result.Count );
			return Encoding.UTF8.GetString ( bytesReceived.Array ?? throw new OutOfMemoryException ( ), 0, result.Count );
		}

		public static async Task SendStringAsync ( this WebSocketSharp.WebSocket ws, string str )
		{
			var finished = false;
			ws.SendAsync ( str, b => finished = b );
			await Task.Run ( ( ) =>
				{
					while ( !finished )
						Thread.Sleep ( 1 );
				}
			);
		}
	}
}