using FluentAssertions;
using Domain.FirstLearning;
namespace ProjectTest_XUnit.FirstLearning
{
    public class FlightSpecification
    {
        [Fact]
        public void Booking_reduces_the_number_of_seats()
        {
            var flight = new Flight(seatCapacity: 3);

            flight.Book("Keyhan@Test.com", 1);

            flight.RemainingNumberOfSeats.Should().Be(2);
        }



        [Fact]
        public void Avoid_OverBooking()
        {
            // Given
            var flight = new Flight(3);

            // When
            var error = flight.Book("keyhan@gmail,com", 4);

            // Then
            error.Should().BeOfType<OverBookingError>();

        }





    }
}
