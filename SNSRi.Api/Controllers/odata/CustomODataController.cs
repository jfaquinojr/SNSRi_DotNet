﻿using Microsoft.Data.OData.Query;
using Microsoft.Data.OData.Query.SemanticAst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

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
		public static string WhereClause(this ODataQueryOptions options)
		{
			string s = "";
			if (options.Filter != null && options.Filter.FilterClause != null)
			{
				var node = options.Filter.FilterClause.Expression as BinaryOperatorNode;
				s = getWhereClause(node);
			}
			return s;
		}

		public static string OrderByClause(this ODataQueryOptions options)
		{
			string s = "";
			// PARSE Order by
			// Order by, e.g. /Products?$orderby=Supplier asc,Price desc
			if (options.OrderBy != null && options.OrderBy.OrderByClause != null)
			{
				foreach (var node in options.OrderBy.OrderByNodes)
				{
					var typedNode = node as OrderByPropertyNode;
					s += $" {typedNode.Property.Name} {getStringValue(typedNode.OrderByClause.Direction)}, ";
				}
			}
			return s.TrimEnd(',', ' ');
		}

		private static string getWhereClause(BinaryOperatorNode node)
		{
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
					s += getWhereClause(node.Left as BinaryOperatorNode);

				if (node.Right is BinaryOperatorNode)
				{
					s += $" {getStringValue(node.OperatorKind)} ";
					s += getWhereClause(node.Right as BinaryOperatorNode);
				}
			}
			return s;
		}

        private static string getStringValue(ConstantNode node)
        {
            var retval = "'" + node.Value + "'";
            switch (node.Value.GetType().Name)
            {
                case "DateTime":
                    DateTime dt = (DateTime) node.Value;
                    retval = "DateTime('" + dt.ToString("yyyy-MM-dd hh:mm:ss") + "')";
                    break;
                default:
                    break;
            }
            return retval;
        }

		private static string getStringValue(BinaryOperatorKind op)
		{
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

			return s;
		}
		private static string getStringValue(OrderByDirection dir)
		{
			if (dir == OrderByDirection.Ascending)
			{
				return "ASC";
			}

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