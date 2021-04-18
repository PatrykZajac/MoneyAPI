using System;
namespace MoneyAPI.Exceptions
{
    public class APIReturnsEmptyDataException : Exception
    {
        public APIReturnsEmptyDataException() : base("API returned empty result")
        {
        }
    }
}
