﻿using System.Collections.Generic;
using System.Threading.Tasks;
using QAEngine.Core.Models;

namespace QAEngine.Core.Services
{
    public interface IQuestionsService
    {
        Task<IEnumerable<QuestionRead>> GetAsync();

        Task<QuestionRead> GetByIdAsync(int id);
    }
}
