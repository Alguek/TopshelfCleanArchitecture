using System.Collections.Generic;

namespace TopshelfCleanArchitecture.Application.UseCase.Base
{
    public class ResultBase
    {
        public IEnumerable<string> Errors { get; set; }

        public ResultBase(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }

    public class ResultBase<T> : ResultBase
    {
        public ResultBase(T result, IEnumerable<string> errors = null) : base(errors)
        {
            Result = result;
        }

        public T Result { get; }
    }
}
