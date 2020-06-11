using System;

namespace QAEngine.Domain.Persistence
{
    public class QuestionCreate
    {
        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
