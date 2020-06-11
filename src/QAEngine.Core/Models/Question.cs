using System;

namespace QAEngine.Core.Models
{
    public class Question
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public bool IsClosed { get; set; }
    }
}
