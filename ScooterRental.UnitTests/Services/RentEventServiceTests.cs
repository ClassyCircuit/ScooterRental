using Moq;
using rentEventRental.UnitTests.Builders;
using ScooterRental.Core.Entities;
using ScooterRental.Infrastructure.Services;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScooterRental.UnitTests.Services
{
    public class RentEventServiceTests : TestBase
    {
        public RentEventServiceTests(Context context) : base(context)
        {
        }
        
        [Fact]
        public void CreateRentEvent_AddsNewEventToList()
        {
            var events = new List<RentEvent>();
            RentEvent newEvent = RentEventBuilder.Default().Build();

            RentEventService service = new RentEventService(events);

            service.CreateEvent(newEvent);

            events.ShouldContain(newEvent);
        }
    }
}
