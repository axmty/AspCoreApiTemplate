using System;

namespace QAEngine.Domain.Persistence
{
    public class Question
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public bool IsClosed { get; set; }
    }
}
