using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using grpcBlazorTest.Shared;
using GrpcTest;

namespace grpcBlazorTest.Server.Servisi
{
	public class MojServis : ProbniServis.ProbniServisBase
	{
		private readonly Konvertor _kon;
		private readonly DB _baza;
		public MojServis(Konvertor k, DB baza)
		{
			_kon = k;
			_baza = baza;
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
	}
}
