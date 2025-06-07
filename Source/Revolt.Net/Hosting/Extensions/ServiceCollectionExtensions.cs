using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Hosting.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.Hosting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRevolt(this IServiceCollection services, Action<IRevoltBuilder> action)
        {
            var builder = new RevoltBuilder(services);

            action(builder);

            return services;
        }
    }
}
