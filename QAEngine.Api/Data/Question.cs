using System;

namespace QAEngine.Api.Data
{
    public class Question
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
