using System;
using System.Collections.Generic;
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
		public MojServis(Konvertor k)
		{
			_kon = k;
		}

		public override Task<ProbnaPoruka> ProbniPoziv(ProbnaPoruka request, ServerCallContext context)
		{
			return  Task.FromResult(new ProbnaPoruka { Nesto = request.Nesto.ToUpper(), NekiBroj = request.NekiBroj *= 2 });
		}

		public override Task<KorisnikPoruka> KorisnikTest(KorisnikPoruka request, ServerCallContext context)
		{
			Korisnik k = _kon.Konvert(request);
			k.Ime = "Ovo je SERVER!!";

			return Task.FromResult(_kon.Konvert(k));
		}
	}
}
