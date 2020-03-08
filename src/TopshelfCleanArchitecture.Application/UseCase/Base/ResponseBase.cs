using System;
using System.Collections.Generic;
using System.Text;

namespace TopshelfCleanArchitecture.Application.UseCase.Base
{
    public class ResponseBase
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
