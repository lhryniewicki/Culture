using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Models
{
	public class Emoticon
	{
		public int Id { get; set; }
		public Emoticon Name { get; set; }
		//zdecydowac o sposobie przechowania obrazka url vs byte[]
	}
}
