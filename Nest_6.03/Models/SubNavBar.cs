using System;
namespace Nest_6._03.Models
{
	public class SubNavBar
	{
		public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int NavbarId { get; set; }
        public NavBar navBar { get; set; } = null!;
    }
}

