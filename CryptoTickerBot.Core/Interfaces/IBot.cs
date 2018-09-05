﻿using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using CryptoTickerBot.Enums;

namespace CryptoTickerBot.Core.Interfaces
{
	public interface IBot : IDisposable
	{
		ICryptoExchange this [ CryptoExchangeId index ] { get; }

		CancellationTokenSource Cts { get; }
		ImmutableDictionary<CryptoExchangeId, ICryptoExchange> Exchanges { get; }
		bool IsInitialized { get; }
		bool IsRunning { get; }
		ImmutableHashSet<IBotService> Services { get; }

		event OnUpdateDelegate Changed;
		event OnUpdateDelegate Next;
		event TerminateDelegate Terminate;

		Task StartAsync ( CancellationTokenSource cts = null );

		Task StartAsync ( CancellationTokenSource cts = null,
		                  params CryptoExchangeId[] exchangeIds );

		void Stop ( );

		bool ContainsService ( IBotService service );
		Task Attach ( IBotService service );
		Task Detach ( IBotService service );
		Task DetachAll<T> ( ) where T : IBotService;
	}
}