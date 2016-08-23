using SNSRi.Entities;
using SNSRi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SNSRi.Api.Controllers
{
    public class UsersController : ApiController
    {
		// GET api/users/5
		public IQueryable<User> Get()
		{
			var page = this.Request.RequestUri.GetQueryIntegerValue("page");
			var perPage = this.Request.RequestUri.GetQueryIntegerValue("per_page");

			var userQuery = new UserQuery(page, perPage);
			return userQuery.Search().AsQueryable();
		}

		// GET api/users/5
		public User Get(int id)
		{
			var userQuery = new UserQuery();
			return userQuery.GetById(id);
		}

		// POST api/users
		public void Post([FromBody]User user)
		{
		}

		// PUT api/users/5
		public void Put(int id, [FromBody]User user)
		{
		}

		// DELETE api/users/5
		public void Delete(int id)
		{
		}

	}

	public static class UriExtensions
	{
		public static string GetQueryStringValue(this Uri UriExension, string name)
		{
			var query = UriExension.Query.Replace('?', '&');
			var result = from r in query.Split('&')
							 where r.Contains($"{name}=")
							 select r.Replace($"{name}=", "");
			return result.FirstOrDefault();
		}

		public static int GetQueryIntegerValue(this Uri UriExension, string name)
		{
			var s = GetQueryStringValue(UriExension, name);
			int i;
			int.TryParse(s, out i);
			return i;
		}
	}
}