using ScooterRental.Core.Entities;
using System;

namespace ScooterRental.Core.Services.Builders
{
    /// <summary>
    /// Builder for creating new rent event entities.
    /// </summary>
    public class RentEventBuilder
    {
        private RentEvent rentEvent;

        private DateTime StartDate;
        private DateTime? EndDate;
        private decimal PricePerMinute;
        private bool IsActive;
        private decimal TotalPrice;
        private string Id;
        private Company company;
        private string scooterId;

        public RentEvent Build()
        {
            if (rentEvent == null)
            {
                rentEvent = new RentEvent(StartDate, EndDate, PricePerMinute, IsActive, Id, company, scooterId);
                rentEvent.TotalPrice = TotalPrice;
            }

            return rentEvent;
        }

        public static RentEventBuilder Default(Company company, Scooter scooter)
        {
            return new RentEventBuilder()
                .WithId(GetRandom.UniqueId())
                .WithPricePerMinute(GetRandom.Decimal(0, 5))
                .WithIsActive(true)
                .WithStartDate(DateTime.Now)
                .WithEndDate(null)
                .WithTotalPrice(0m)
                .WithCompany(company)
                .WithScooterId(scooter.Id);
        }

        private RentEventBuilder WithScooterId(string value)
        {
            scooterId = value;
            return this;
        }

        public RentEventBuilder WithCompany(Company value)
        {
            company = value;
            return this;
        }

        public RentEventBuilder WithId(string value)
        {
            Id = value;
            return this;
        }

        public RentEventBuilder WithPricePerMinute(decimal value)
        {
            PricePerMinute = value;
            return this;
        }

        public RentEventBuilder WithStartDate(DateTime value)
        {
            StartDate = value;
            return this;
        }

        public RentEventBuilder WithEndDate(DateTime? value)
        {
            EndDate = value;
            return this;
        }

        public RentEventBuilder WithIsActive(bool value)
        {
            IsActive = value;
            return this;
        }

        public RentEventBuilder WithTotalPrice(decimal value)
        {
            TotalPrice = value;
            return this;
        }
    }
}
