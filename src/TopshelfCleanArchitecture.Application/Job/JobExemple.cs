using Quartz;
using System;
using System.Threading.Tasks;
using TopshelfCleanArchitecture.Application.Interfaces;
using TopshelfCleanArchitecture.Application.Interfaces.Repository;
using TopshelfCleanArchitecture.Application.Job.Base;

namespace TopshelfCleanArchitecture.Application.Job
{
    [DisallowConcurrentExecution]
    public class JobExemple : JobBase
    {
        private readonly IProductRepository _productRepository;
        public JobExemple(string jobId, ILoggerFile loggerFile, IProductRepository productRepository) : base(jobId, loggerFile)
        {
            _productRepository = productRepository;
        }

        public async override Task ExecutarAsync()
        {
            await _productRepository.ObterPorId(1);
            throw new NotImplementedException();
        }
    }
}
