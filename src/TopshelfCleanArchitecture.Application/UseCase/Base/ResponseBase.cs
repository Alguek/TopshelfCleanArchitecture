using System.Collections.Generic;

namespace TopshelfCleanArchitecture.Application.UseCase.Base
{
    public class ResponseBase
    {
        public IEnumerable<string> Errors { get; set; }
        public object Result { get; set; }

        public ResponseBase(object result)
        {
            Result = result;
        }

        public ResponseBase(IEnumerable<string> erros)
        {
            Errors = erros;
        }

        public ResponseBase(){ }
    }
}
