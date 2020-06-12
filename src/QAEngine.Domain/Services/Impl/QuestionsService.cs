using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QAEngine.Domain.Exceptions;
using QAEngine.Domain.Persistence;
using QAEngine.Domain.Resources;

namespace QAEngine.Domain.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IMapper _mapper;
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsService(
            IMapper mapper,
            IQuestionsRepository questionsRepository)
        {
            _mapper = mapper;
            _questionsRepository = questionsRepository;
        }

        public async Task<IEnumerable<QuestionResponse>> ListAsync()
        {
            return (await _questionsRepository.ListAsync()).Select(_mapper.Map<QuestionResponse>);
        }

        public async Task<QuestionResponse> GetByIdAsync(int id)
        {
            var data = await _questionsRepository.GetByIdAsync(id);

            if (data is null)
            {
                throw new NotFoundException($"Question [{id}] does not exist.");
            }

            return _mapper.Map<QuestionResponse>(data);
        }

        public async Task<int> CreateAsync(QuestionCreateRequest question)
        {
            return await _questionsRepository.CreateAsync(_mapper.Map<QuestionCreate>(question));
        }
    }
}
