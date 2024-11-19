using System.ComponentModel.DataAnnotations;

namespace IntroASP.Models.ViewModels
{
	public class BeerDeleteViewModel
	{
		[Required]
		[Display(Name = "ID de la bebida")]
		public string Id { get; set; }
	}
}
