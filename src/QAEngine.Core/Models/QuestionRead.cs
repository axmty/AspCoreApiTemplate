using System;

namespace QAEngine.Core.Models
{
    public class QuestionRead
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
