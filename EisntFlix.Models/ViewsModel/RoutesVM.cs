using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntFlix.Models.ViewsModel
{
	public class RoutesVM
	{
		public string Area { get; set; }

		public string Controller { get; set; }

		public string Action { get; set; }

		public int Id { get; set; }

		public string? RouteName { get; set; }
	}
}
