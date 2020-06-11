using System;

namespace QAEngine.Domain.Persistence
{
    public class Answer
    {
        public int ID { get; set; }

        public string Content { get; set; } 

        public DateTimeOffset CreateDate { get; set; }
    }
}
