using System;

namespace QAEngine.Core.Data
{
    public class Question
    {
        public int QuestionID { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
