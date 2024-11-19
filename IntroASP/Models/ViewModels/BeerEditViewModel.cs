using System.ComponentModel.DataAnnotations;

namespace IntroASP.Models.ViewModels
{
	public class BeerEditViewModel
	{
		[Required]
		[Display(Name = "ID de la bebida")]
		public string Id { get; set; }

		[Required]
		[Display(Name = "Nombre")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Marca")]
		public int brandid { get; set; }
	}
}
