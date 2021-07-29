using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace QoolloEDU.WebService
{
    public class AuthOptions
    {
        public const string ISSUER = "Server"; // token publisher
        public const string AUDIENCE = "Client"; // token consumer
        public const string KEY = "this is QoolloEDU Secret key for authnetication";   // key for encoding
        public const int LIFETIME = 1000; // token lifetime - 1 minute
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}