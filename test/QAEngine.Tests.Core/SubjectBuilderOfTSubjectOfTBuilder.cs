using System;
using System.Reflection;
using AutoMapper;
using QAEngine.Domain.Mapping;

namespace QAEngine.Tests.Core
{
    public abstract class SubjectBuilder<TSubject, TBuilder>
    {
        public static TBuilder Configure(Action<TBuilder> options)
        {
            var builder = (TBuilder)Activator.CreateInstance(typeof(TBuilder));

            typeof(TBuilder).GetMethod(nameof(Init), BindingFlags.Instance | BindingFlags.NonPublic).Invoke(builder, null);
            options(builder);

            return builder;
        }

        public abstract TSubject Build();
        
        protected abstract void Init();

        protected IMapper BuildMapper()
        {
            return new MapperConfiguration(cfg => cfg.AddMaps(typeof(DomainProfile))).CreateMapper();
        }
    }
}
