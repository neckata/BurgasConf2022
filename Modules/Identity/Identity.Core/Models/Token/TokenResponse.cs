using System;

namespace Identity.Core.Models.Token
{
    public class TokenResponse
    {
        public TokenResponse(string token, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            Token = token;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}