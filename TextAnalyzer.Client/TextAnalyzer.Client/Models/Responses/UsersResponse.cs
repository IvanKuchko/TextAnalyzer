using System.Collections.Generic;

namespace TextAnalyzer.Client.Models.Responses
{
    public class UsersResponse : BaseResponse
    {
        public IEnumerable<User> Users { get; set; }
    }
}
