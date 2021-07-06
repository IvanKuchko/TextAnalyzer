using System.Collections.Generic;

namespace TextAnalyzer.Server.Models.Responses
{
    public class UsersResponse : BaseResponse
    {
        public IEnumerable<User> Users { get; set; }
    }
}
