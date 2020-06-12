using AutoMapper;
using QAEngine.Domain.Persistence;
using QAEngine.Domain.Resources;

namespace QAEngine.Domain.Mapping
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            this.CreateMap<Question, QuestionResponse>();
            this.CreateMap<QuestionCreateRequest, QuestionCreate>();
        }
    }
}
