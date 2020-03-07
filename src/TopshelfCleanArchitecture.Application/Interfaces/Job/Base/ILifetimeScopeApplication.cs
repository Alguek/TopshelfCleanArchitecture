using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopshelfCleanArchitecture.Application.Interfaces.Job
{
    public interface ILifetimeScopeApplication
    {
        IJob ResolveJob(Type type);
    }
}
