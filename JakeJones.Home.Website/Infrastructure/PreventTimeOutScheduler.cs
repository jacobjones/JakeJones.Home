using System;
using System.Net.Http;
using System.Threading;

namespace JakeJones.Home.Website.Infrastructure
{
	/// <summary>
	/// Prevents the IIS Idle Time-out by pinging the website at scheduled intervals.
	/// </summary>
	internal class PreventTimeOutScheduler : IDisposable
	{
		private bool _cancelled;
		private int _pingFrequency = 60;

		private readonly AutoResetEvent _waitHandle = new(false);
		private readonly object _syncLock = new();

		private HttpClient _httpClient;

		public void Start(int pingFrequency)
		{
			_pingFrequency = pingFrequency;
			_cancelled = false;

			_httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://jakejon.es")
			};
			
			var thread = new Thread(Run);
			thread.Start();
		}

		public void Stop()
		{
			lock (_syncLock)
			{
				if (_cancelled)
				{
					return;
				}
				
				_cancelled = true;
				_waitHandle.Set();
				_httpClient.Dispose();
			}
		}

		private void Run()
		{
			while (!_cancelled)
			{
				PingServer();

				_waitHandle.WaitOne(_pingFrequency * 1000, true);
			}
		}

		private void PingServer()
		{
			try
			{
				_httpClient.GetStringAsync("about");
			}
			catch (Exception)
			{
				// We can safely ignore any exceptions here.
			}
		}

		public void Dispose()
		{
			Stop();
		}
	}
}