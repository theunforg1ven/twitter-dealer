using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterDealer.Data.Entities
{
	public class User
	{
		public int Id { get; set; }

		public string Username { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public string Gender { get; set; }

		public DateTime Created { get; set; }

		public DateTime LastActive { get; set; }

		public ICollection<Photo> Photos { get; set; }

		public ICollection<SavedThread> SavedThreads { get; set; }
	}
}
