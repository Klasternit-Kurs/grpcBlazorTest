using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace grpcBlazorTest.Shared
{
	public class Korisnik
	{
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public List<Adresa> Adrese { get; set; } = new List<Adresa>();
	}

	public class Adresa
	{
		public string Ulica { get; set; }
		public string Broj { get; set; }
	}
}
