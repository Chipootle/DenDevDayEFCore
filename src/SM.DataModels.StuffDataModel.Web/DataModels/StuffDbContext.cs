﻿using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using SM.DataModels.StuffDataModel.Entities;

namespace SM.DataModels.StuffDataModel
{
	public class StuffDbContext : DbContext, IStuffDataModel
	{
		// Since this DbContext isfor the basic example, this contains "repository" type methods

		public StuffDbContext(DbContextOptions<StuffDbContext> options)
			: base(options) { }

		public DbSet<Person> People { get; set; }

		public DbSet<Status> Statuses { get; set; }

		public DbSet<Entities.Stuff> Stuff { get; set; }

		public bool AddPerson(Person person)
		{
			person.DateJoined = DateTime.Now;

			People.Add(person);

			return true;
		}

		public bool AddStuff(Entities.Stuff stuff)
		{
			stuff.DateAdded = DateTime.Now;

			Stuff.Add(stuff);

			return true;
		}

		public void Commit()
		{
			SaveChanges();
		}

		public Person GetPerson(int id)
		{
			return People.FirstOrDefault(item => item.Id == id);
		}

		public Person GetPerson(string lastName)
		{
			return People.FirstOrDefault(item => item.LastName == lastName);
		}

		public Stuff GetStuff(int id)
		{
			// TODO: Need to include Owner.

			return Stuff.FirstOrDefault(item => item.Id == id);
		}

		//----==== PROTECTED ====------------------------------------------------------------------

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Old way...
			//modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			modelBuilder.Model.RemoveEntityType("OneToManyCascadeDeleteConvention");
		}
	}
}