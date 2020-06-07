using System;

namespace QAEngine.Api.Models
{
    public class Question
    {
        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
