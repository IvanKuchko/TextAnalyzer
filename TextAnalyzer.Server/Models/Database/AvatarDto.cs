using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyzer.Server.Models.Database
{
    public class AvatarDto : BaseDto
    {
        public UserDto User { get; set; }
        public string CharPair { get; set; }
        public int Delay { get; set; }
        public int Identical { get; set; }
    }
}
