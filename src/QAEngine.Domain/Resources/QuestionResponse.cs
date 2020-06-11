using System;

namespace QAEngine.Domain.Resources
{
    public class QuestionResponse
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public bool IsClosed { get; set; }
    }
}
