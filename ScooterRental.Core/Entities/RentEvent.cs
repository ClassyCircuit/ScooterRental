using System;

namespace ScooterRental.Core.Entities
{
    /// <summary>
    /// Represents a rental period for a scooter. 
    /// Can be active or completed.
    /// If rental period spans multiple days, then a new rent event is created for each day.
    /// </summary>
    public class RentEvent
    {
        public RentEvent(DateTime startDate, DateTime? endDate, decimal pricePerMinute, bool isActive, string id, Company company, string scooterId)
        {
            StartDate = startDate;
            EndDate = endDate;
            PricePerMinute = pricePerMinute;
            IsActive = isActive;
            Id = id;
            Company = company;
            ScooterId = scooterId;
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

        private decimal _pricePerMinute;
        /// <summary>
        /// Agreed price per minute for the entire rent period.
        /// </summary>
        public decimal PricePerMinute
        {
            get => _pricePerMinute;
            set => _pricePerMinute = Math.Round(value, 3);
        }

        /// <summary>
        /// True if rent is still in-progress.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Total price for the entire rent period.
        /// </summary>
        public decimal TotalPrice { get; set; }

        public Company Company { get; set; }

        public string ScooterId { get; set; }
    }
}
