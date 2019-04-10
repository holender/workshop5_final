using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Work3.Core.Entities;

namespace Work3.Infrastructure
{
	public class StudentContext : DbContext
	{
		public StudentContext(
			DbContextOptions<StudentContext> options)
			: base(options)
		{

		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Subject> Subjects { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Student>();
			builder.Entity<Teacher>();
			builder.Entity<Subject>();
		}
	}
}
