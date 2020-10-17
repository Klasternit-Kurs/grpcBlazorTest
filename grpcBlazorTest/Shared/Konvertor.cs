using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrpcTest;

namespace grpcBlazorTest.Shared
{
	public class Konvertor
	{
		public KorisnikPoruka Konvert(Korisnik k)
		{
			KorisnikPoruka kp = new KorisnikPoruka {Ime = k.Ime, Prezime = k.Prezime };
			k.Adrese.ForEach(a => kp.Adrese.Add(Konvert(a)));
			return kp;
		}

		public AdresaPoruka Konvert(Adresa a)
			=> new AdresaPoruka { Ulica = a.Ulica, Broj = a.Broj };

		public Korisnik Konvert (KorisnikPoruka kp)
		{
			Korisnik k = new Korisnik {Ime = kp.Ime, Prezime = kp.Prezime };
			kp.Adrese.ToList().ForEach(adrp => k.Adrese.Add(Konvert(adrp)));
			return k;
		}

		public Adresa Konvert(AdresaPoruka ap)
			=> new Adresa { Ulica = ap.Ulica, Broj = ap.Broj };
	}
}
