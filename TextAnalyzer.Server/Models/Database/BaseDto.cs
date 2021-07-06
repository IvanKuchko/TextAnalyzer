using System;

namespace TextAnalyzer.Server.Models.Database
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
