using System;
using System.Net;
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

		private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
		private readonly object _syncLock = new object();

		public void Start(int pingFrequency)
		{
			_pingFrequency = pingFrequency;
			_cancelled = false;

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
		public void PingServer()
		{
			try
			{
				new WebClient().DownloadString("http://jakejon.es/about");
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