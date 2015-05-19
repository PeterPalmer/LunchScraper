using System;
using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

namespace LunchScraper.Core.Domain
{
	public class Dish : TableEntity, IEquatable<Dish>
	{
		public override int GetHashCode()
		{
			unchecked
			{
				return ((Description != null ? Description.GetHashCode() + RestaurantId : 0) * 397) ^ Date.GetHashCode();
			}
		}

		public String Description { get; set; }
		public DateTime Date { get; set; }
		public int RestaurantId { get; set; }

		public Dish()
		{
		}

		public Dish(string description, DateTime date, int restaurantId)
		{
			this.Description = WebUtility.HtmlDecode(description).Trim();
			this.Date = date;
			this.RestaurantId = restaurantId;

			this.PartitionKey = date.ToString("yyyyMMdd");
			this.RowKey = GetHashCode().ToString();
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