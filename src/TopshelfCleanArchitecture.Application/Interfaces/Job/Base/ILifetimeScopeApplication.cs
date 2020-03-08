using Quartz;
using System;

namespace TopshelfCleanArchitecture.Application.Interfaces.Job
{
    public interface ILifetimeScopeApplication
    {
        IJob ResolveJob(Type type);
    }
}
