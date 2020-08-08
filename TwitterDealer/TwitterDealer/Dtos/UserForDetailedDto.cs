using System;
using System.Collections.Generic;
using TwitterDealer.Data.Entities;

namespace TwitterDealer.Dtos
{
	public class UserForDetailedDto
	{
		public int Id { get; set; }

		public string Username { get; set; }

		public string Gender { get; set; }

		public DateTime Created { get; set; }

		public DateTime LastActive { get; set; }

		public string PhotoUrl { get; set; }

		public ICollection<PhotosForDetailedDto> Photos { get; set; }

		public ICollection<SavedThread> SavedThreads { get; set; }
	}
}
