using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.ApiClient.Tests
{
    public class TestBase
    {
        private readonly IServiceCollection _services;
        public ServiceProvider ServiceProvider { get; }

        public TestBase()
        {
            _services = new ServiceCollection();
            ConfigureServices(_services);
            ServiceProvider = _services.BuildServiceProvider();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
