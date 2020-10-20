using grpcBlazorTest.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpcBlazorTest.Server
{
	public class DB : DbContext
	{
		public DB(DbContextOptions<DB> dbo) : base(dbo) { }

		public DbSet<Korisnik> Korisniks { get; set; }
		public DbSet<Adresa> Adresas { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Korisnik>().HasKey(k => k.ID);
			modelBuilder.Entity<Adresa>().HasKey(a => a.ID);

			modelBuilder.Entity<Korisnik>().HasMany(k => k.Adrese)
										   .WithOne(a => a.Vlasnik)
										   .HasForeignKey(a => a.VlasnikFK);

			modelBuilder.Entity<Korisnik>().HasData(
				new Korisnik {ID = "a", Ime = "Pera", Prezime = "Peric" },
				new Korisnik {ID = "b", Ime = "Neko", Prezime = "Nekic" },
				new Korisnik {ID = "c", Ime = "Trecko", Prezime = "Treckovic" },
				new Korisnik {ID = "d", Ime = "Bla", Prezime = "BlaBla" }
			);

			modelBuilder.Entity<Adresa>().HasData(
				new Adresa {ID = -1, Ulica = "a", Broj = "1", VlasnikFK = "a" },
				new Adresa {ID = -2, Ulica = "b", Broj = "2", VlasnikFK = "a" },
				new Adresa {ID = -3, Ulica = "c", Broj = "3", VlasnikFK = "a" },
				new Adresa {ID = -4, Ulica = "d", Broj = "4", VlasnikFK = "b" },
				new Adresa {ID = -5, Ulica = "e", Broj = "5", VlasnikFK = "c" },
				new Adresa {ID = -6, Ulica = "f", Broj = "6", VlasnikFK = "c" },
				new Adresa {ID = -7, Ulica = "g", Broj = "7", VlasnikFK = "d" },
				new Adresa {ID = -8, Ulica = "h", Broj = "8", VlasnikFK = "d" }
			);
		}
	}
}
