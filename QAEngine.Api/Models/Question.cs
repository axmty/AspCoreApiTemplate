﻿using System;

namespace QAEngine.Api.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}
