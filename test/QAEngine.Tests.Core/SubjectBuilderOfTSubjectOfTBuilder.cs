using System;

namespace QAEngine.Tests.Core
{
    public abstract class SubjectBuilder<TSubject, TBuilder>
    {
        public static TBuilder Configure(Action<TBuilder> options)
        {
            var builder = (TBuilder)Activator.CreateInstance(typeof(TBuilder));

            options(builder);

            return builder;
        }

        public abstract TSubject Build();
    }
}
