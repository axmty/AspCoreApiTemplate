using System;

namespace QAEngine.Core.Data
{
    public class QuestionCreate
    {
        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
