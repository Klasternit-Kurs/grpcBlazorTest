using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using GrpcTest;
using grpcBlazorTest.Shared;

namespace grpcBlazorTest.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			builder.Services.AddSingleton(tk =>
			{
				string server = tk.GetRequiredService<NavigationManager>().BaseUri;
				var httpKlijent = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));

				var kanal = GrpcChannel.ForAddress(server, new GrpcChannelOptions { HttpClient = httpKlijent });

				return new ProbniServis.ProbniServisClient(kanal);

			});
			builder.Services.AddTransient<Konvertor>();
			await builder.Build().RunAsync();
		}

		

	}
}
