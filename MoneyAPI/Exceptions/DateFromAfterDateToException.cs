using System;
namespace MoneyAPI.Exceptions
{
    public class DateFromAfterDateToException : Exception
    {
        public DateFromAfterDateToException() : base("DateFrom is after DateTo")
        {
        }
    }
}
