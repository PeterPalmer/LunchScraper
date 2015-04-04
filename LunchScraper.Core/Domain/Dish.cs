using System;
using System.Net;

namespace LunchScraper.Core.Domain
{
	public class Dish : IEquatable<Dish>
	{
		public override int GetHashCode()
		{
			unchecked
			{
				return ((Description != null ? Description.GetHashCode() : 0)*397) ^ Date.GetHashCode();
			}
		}

		public String Description { get; set; }
		public DateTime Date { get; set; }

		public Dish()
		{
		}

		public Dish(string description, DateTime date)
		{
			Description = WebUtility.HtmlDecode(description).Trim();
			Date = date;
		}

		public override string ToString()
		{
			return Description;
		}

		public bool Equals(Dish other)
		{
			if (other == null)
			{
				return false;
			}

			return string.Equals(Description, other.Description) && Date.Equals(other.Date);
		}

		public override bool Equals(Object other)
		{
			if (other is Dish)
			{
				return Equals((Dish)other);
			}

			return false;
		}
	}
}