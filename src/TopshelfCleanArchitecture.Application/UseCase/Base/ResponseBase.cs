using System.Collections.Generic;

namespace TopshelfCleanArchitecture.Application.UseCase.Base
{
    public class ResponseBase
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
