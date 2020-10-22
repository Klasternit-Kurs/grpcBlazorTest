using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpcBlazorTest.Server.Servisi
{
	public class rHub : Hub
	{
		public void Sabirac(List<int> brojevi)
		{
			int zbir = brojevi.Aggregate((t, i) => t += i);
			Clients.Caller.SendAsync("EvoZbir", zbir);
		}
	}
}
