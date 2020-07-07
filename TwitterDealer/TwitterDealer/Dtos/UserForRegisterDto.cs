using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterDealer.Dtos
{
	public class UserForRegisterDto
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[StringLength(24, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 24 characters")]
		public string Password { get; set; }
	}
}
