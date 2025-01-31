using Microsoft.AspNetCore.Http;

namespace Infrastructure.Exceptions
{
	public class ServiceException: BaseDomainException
    {
        public int StatusCode { get; private set; } = StatusCodes.Status400BadRequest;

		public ServiceException(string? message) : base(message)
        {
        }

		public ServiceException(string? message, int statusCode) : base(message)
		{
			StatusCode = statusCode;
		}

		public ServiceException(string? message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
