using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TextAnalyzer.Server
{
    public class AuthOptions
    {
        public const string ISSUER = "TextAnalyzerServer"; 
        public const string AUDIENCE = "TextAnalyzerClient";
        const string KEY = "ServerSecret123!ServerSecret123!";   
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
