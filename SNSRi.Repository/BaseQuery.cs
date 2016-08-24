using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository
{
	public abstract class BaseQuery<T>
	{
		public abstract IEnumerable<T> Search(string where = "", string order = "");
		public abstract T GetById(int Id);
	}
}
