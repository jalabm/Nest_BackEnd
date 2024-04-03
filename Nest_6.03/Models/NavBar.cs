using System;
namespace Nest_6._03.Models
{
	public class NavBar
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
        public int Order { get; set; } 

        public ICollection<SubNavBar> subNavBars { get; set; }
        public NavBar()
        {
            subNavBars = new HashSet<SubNavBar>();
        }
    }
}

