using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IntroASP.Models.ViewModels
{
	public class BeerViewModel
	{
		[Required]
		[Display(Name = "Nombre")]
		public string Name {  get; set; }

		[Required]
		[Display(Name = "Marca")]
		public int brandid { get; set; }
	}
}
