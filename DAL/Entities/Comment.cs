using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Comment
	{
		public Guid Comment_Id { get; set; }
		public string Title {  get; set; }
		public string Content { get; set; }
		public Guid Concern { get; set; }
		public DateTime CreatedAt { get; set; }
		public Guid? CreatedBy { get; set; }
		public short? Note {  get; set; }
	}
}
