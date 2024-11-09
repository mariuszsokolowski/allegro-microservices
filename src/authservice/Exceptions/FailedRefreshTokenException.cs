using Microsoft.VisualBasic;
using System.Net;

namespace authservice.Exceptions
{
    public sealed class FailedRefreshTokenException : CustomException
    {
        public FailedRefreshTokenException(HttpStatusCode statusCode,string contents) : base($"Request failed to get AccessToken by RefreshToken with status code {statusCode}. Response: {contents}")
        {
        }
    }
}
