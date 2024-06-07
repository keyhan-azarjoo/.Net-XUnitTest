namespace Domain.FirstLearning
{
    public class Flight
    {
        public int RemainingNumberOfSeats { get; set; }
        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public object? Book(string passengerEmail, int numberOfSeats)
        {

            if (numberOfSeats > RemainingNumberOfSeats)
                return new OverBookingError();

            RemainingNumberOfSeats -= numberOfSeats;
            return null;
            //throw new NotImplementedException("no end point yet");
        }
    }
}
