﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.UI.Models
{
	public class LoginViewModel
	{
		[Required]
		public string? Username {  get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
