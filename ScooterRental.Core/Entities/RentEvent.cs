using System;

namespace ScooterRental.Core.Entities
{
    public class RentEvent
    {
        public RentEvent(DateTime startDate, DateTime? endDate, decimal pricePerMinute, bool isActive, string id, Company company)
        {
            StartDate = startDate;
            EndDate = endDate;
            PricePerMinute = pricePerMinute;
            IsActive = isActive;
            Id = id;
            Company = company;
        }

        /// <summary>
        /// Unique Id
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Date when rent started.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Nullable date when rent ended.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Agreed price per minute for the entire rent period.
        /// </summary>
        public decimal PricePerMinute { get; set; }

        /// <summary>
        /// True if rent is still in-progress.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Total price for the entire rent period.
        /// </summary>
        public decimal TotalPrice { get; set; }

        public Company Company { get; set; }
    }
}
