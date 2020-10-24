using System;

namespace ScooterRental.Core.Entities
{
    public class RentEvent
    {
        public RentEvent(DateTime startDate, DateTime? endDate, decimal pricePerMinute, bool isActive, decimal totalPrice, string id)
        {
            StartDate = startDate;
            EndDate = endDate;
            PricePerMinute = pricePerMinute;
            IsActive = isActive;
            TotalPrice = totalPrice;
            Id = id;
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
    }
}
