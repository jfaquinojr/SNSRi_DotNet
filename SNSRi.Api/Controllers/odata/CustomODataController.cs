using Microsoft.Data.OData.Query;
using Microsoft.Data.OData.Query.SemanticAst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using log4net;

namespace SNSRi.odata.Controllers
{
	public class CustomODataController : ODataController
	{
		protected ODataValidationSettings _validationSettings =
			new ODataValidationSettings
			{
				// These validation settings prevent anything except: (equals, and, or) filter and sorting
				AllowedFunctions = AllowedFunctions.None,
				AllowedLogicalOperators = AllowedLogicalOperators.Equal | AllowedLogicalOperators.GreaterThanOrEqual | AllowedLogicalOperators.GreaterThan | AllowedLogicalOperators.And | AllowedLogicalOperators.Or | AllowedLogicalOperators.NotEqual,
				AllowedArithmeticOperators = AllowedArithmeticOperators.None,
				AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy
			};

		protected int _page
		{
			get	{ return this.Request.RequestUri.GetQueryIntegerValue("page"); }
		}

		public int _perPage
		{
			get { return this.Request.RequestUri.GetQueryIntegerValue("per_page"); }
		}

	}

	public static class ODataExtensions
	{
        private static readonly ILog log = LogManager.GetLogger(typeof(ODataExtensions));

        public static string WhereClause(this ODataQueryOptions options)
		{
            log.Info("WhereClause Enter");
            
			string s = "";
			if (options?.Filter?.FilterClause != null)
			{
				var node = options.Filter.FilterClause.Expression as BinaryOperatorNode;
				s = getWhereClause(node);
			}

            log.Debug($"Return value is {s}");
            log.Info("WhereClause Exit");

            return s;
        }

		public static string OrderByClause(this ODataQueryOptions options)
		{
            log.Info("OrderByClause Enter");
            
			string s = "";
			// PARSE Order by
			// Order by, e.g. /Products?$orderby=Supplier asc,Price desc
			if (options?.OrderBy?.OrderByClause != null)
			{
				foreach (var node in options.OrderBy.OrderByNodes)
				{
					var typedNode = node as OrderByPropertyNode;
					s += $" {typedNode.Property.Name} {getStringValue(typedNode.OrderByClause.Direction)}, ";
				}
			}

            log.Debug($"Return value is {s}");
            log.Info("OrderByClause Exit");

			return s.TrimEnd(',', ' ');
		}

		private static string getWhereClause(BinaryOperatorNode node)
		{
            log.Info("getWhereClause Enter");
            
			// PARSE FILTER CLAUSE
			// Parsing a filter, e.g. /Products?$filter=Name eq 'beer'  
			var s = "";
			if (node.Left is SingleValuePropertyAccessNode && node.Right is ConstantNode)
			{
				var property = node.Left as SingleValuePropertyAccessNode ?? node.Right as SingleValuePropertyAccessNode;
				var constant = node.Left as ConstantNode ?? node.Right as ConstantNode;

				if (property != null && property.Property != null && constant != null && constant.Value != null)
				{
					s += $" {property.Property.Name} {getStringValue(node.OperatorKind)} {getStringValue(constant)} ";
				}
			}
			else
			{
                if (node.Left is BinaryOperatorNode)
                {
                    log.Debug("Node Left is a BinaryOperatorNode");
                    s += getWhereClause(node.Left as BinaryOperatorNode);
                }

				if (node.Right is BinaryOperatorNode)
				{
                    log.Debug("Node Right is a BinaryOperatorNode");
                    s += $" {getStringValue(node.OperatorKind)} ";
					s += getWhereClause(node.Right as BinaryOperatorNode);
				}
			}

            log.Debug($"return value is {s}");
            log.Info("getWhereClause Exit");
            
			return s;
		}

        private static string getStringValue(ConstantNode node)
        {
            log.Info("getStringValue Enter");
            
            var retval = "'" + node.Value + "'";

            var typeName = node.Value.GetType().Name;
            log.Debug($"Node Value Type is {typeName}");
            switch (typeName)
            {
                case "DateTime":
                    DateTime dt = (DateTime) node.Value;
                    retval = "DateTime('" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    break;
                default:
                    break;
            }

            log.Debug($"Return value is {retval}");
            log.Info("getStringValue Exit");
            
            return retval;
        }

		private static string getStringValue(BinaryOperatorKind op)
		{
            log.Info("getStringValue Enter");

            log.Debug($"Operator is {op.ToString()}");
			string s;
			switch (op)
			{
				case BinaryOperatorKind.Equal:
					s = " = ";
					break;
				case BinaryOperatorKind.GreaterThan:
					s = " > ";
					break;
				case BinaryOperatorKind.GreaterThanOrEqual:
					s = " >= ";
					break;
				case BinaryOperatorKind.LessThan:
					s = " < ";
					break;
				case BinaryOperatorKind.LessThanOrEqual:
					s = " <= ";
					break;
				case BinaryOperatorKind.NotEqual:
					s = " <> ";
					break;
				default:
					s = $" {op.ToString()} ";
					break;
			}

            log.Debug($"Return value is {s}");
            log.Info("getStringValue Exit");
            
			return s;
		}
		private static string getStringValue(OrderByDirection dir)
		{
            log.Info("getStringValue Enter");

			if (dir == OrderByDirection.Ascending)
			{
				return "ASC";
			}

            log.Debug($"Direction is {dir.ToString()}");
            log.Info("getStringValue Exit");
            
			return "DESC";
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