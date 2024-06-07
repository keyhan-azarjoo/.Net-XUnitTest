namespace Domain.FirstLearning
{
    public class Calculator
    {

        //public int Sum(int left, int right)
        //{
        //    return left + right;
        //}

        public int Sum(int left, int right) => left + right;

        public int mines(int left, int right)
        {
            return left - right;
        }

        public int devide(int left, int right)
        {
            return left / right;
        }

    }
}
