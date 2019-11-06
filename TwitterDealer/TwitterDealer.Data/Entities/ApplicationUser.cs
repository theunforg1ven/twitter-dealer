using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterDealer.Data.Entities
{
	public class ApplicationUser : IdentityUser
	{
		[Column(TypeName = "nvarchar(100)")]
		public string TwitterUsername { get; set; }

		public virtual List<SavedThread>  SavedThreads { get; set; }
	}
}
