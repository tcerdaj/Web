﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JehovaJireh.Core.Entities
{
	public class EntityBase<IdT>
	{
		private IdT _id = default(IdT);


		public virtual IdT Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual DateTime CreatedOnUTC {get;set;}
		public virtual DateTime ModifiedOnUTC { get; set; }
		public virtual string CreatedBy { get; set; }
		public virtual string ModifiedBy { get; set; }

		public virtual string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}

		public virtual IdT ToObject(string json)
		{
			try
			{
				var result = JsonConvert.DeserializeObject<IdT>(json);

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error in BaseEntity line 25, method ToObject(): " + ex.Message);
				throw ex;
			}
		}

		public virtual string ToXml(string rootNode)
		{
			return JsonConvert.DeserializeXNode(this.ToJson(), rootNode).ToString();
		}

		public virtual string ToXml()
		{
			return ToXml(this.GetType().Name);
		}
	}
}
