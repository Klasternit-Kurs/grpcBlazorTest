using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using grpcBlazorTest.Shared;
using GrpcTest;
using Microsoft.Extensions.Logging;

namespace grpcBlazorTest.Server.Servisi
{
	public class MojServis : ProbniServis.ProbniServisBase
	{
		private readonly Konvertor _kon;
		private readonly DB _baza;
		private readonly ILogger<MojServis> _logger;
		public MojServis(Konvertor k, DB baza, ILogger<MojServis> loger)
		{
			_kon = k;
			_baza = baza;
			_logger = loger;
		}

		public override Task<ProbnaPoruka> ProbniPoziv(ProbnaPoruka request, ServerCallContext context)
		{
			return  Task.FromResult(new ProbnaPoruka { Nesto = request.Nesto.ToUpper(), NekiBroj = request.NekiBroj *= 2 });
		}

		public override Task<KorisnikPoruka> KorisnikTest(KorisnikPoruka request, ServerCallContext context)
		{
			var kor = _baza.Korisniks.ToList();
			_baza.Adresas.Where(a => a.Vlasnik == kor[1]).ToArray();

			return Task.FromResult(_kon.Konvert(kor[1]));
		}

		public override async Task KorisnikStream(KorisnikPoruka request, IServerStreamWriter<KorisnikPoruka> responseStream, ServerCallContext context)
		{
			foreach (var k in _baza.Korisniks.ToList())
			{
				_baza.Adresas.Where(a => a.Vlasnik == k).ToList();
				await responseStream.WriteAsync(_kon.Konvert(k));
			}
		}

		public override async Task<BrojPoruka> Sabirac(IAsyncStreamReader<BrojPoruka> requestStream, ServerCallContext context)
		{
			int zbir = 0;
			_logger.LogInformation("Primam stream");
			await foreach(BrojPoruka bp in requestStream.ReadAllAsync())
			{
				_logger.LogInformation($"Dobio broj {bp.Br}");
				zbir += bp.Br;
			}
			_logger.LogInformation("Zavrsen stream, saljem odgovor");
			return new BrojPoruka { Br = zbir };
		}

		public override async Task Dupleks(IAsyncStreamReader<BrojPoruka> requestStream, IServerStreamWriter<BrojPoruka> responseStream, ServerCallContext context)
		{
			await foreach(BrojPoruka bp in requestStream.ReadAllAsync())
			{
				await responseStream.WriteAsync(new BrojPoruka { Br = bp.Br / 2 });
				await responseStream.WriteAsync(new BrojPoruka { Br = bp.Br * 2 });
			}
		}
	}
}
