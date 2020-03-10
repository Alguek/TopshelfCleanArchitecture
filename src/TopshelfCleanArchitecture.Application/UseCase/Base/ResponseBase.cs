using System;
using System.Collections.Generic;

namespace TopshelfCleanArchitecture.Application.UseCase.Base
{
    public class ResponseBase
    {
        public IEnumerable<string> Errors { get; set; }

        public ResponseBase(IEnumerable<string> errors)
        {
             Errors = errors;
        }
    }

    public class ResponseBase<T> : ResponseBase
    {
        public ResponseBase(T result, IEnumerable<string> errors = null) : base(errors)
        {
            Result = result;
        }

        public T Result { get; }
    }
}
