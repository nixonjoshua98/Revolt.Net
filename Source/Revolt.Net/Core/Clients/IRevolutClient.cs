using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.Core.Clients
{
    public interface IRevolutClient
    {
        Task RunAsync(CancellationToken cancellationToken = default);
        Task LoginAsync(CancellationToken cancellationToken = default);
        Task LogoutAsync(CancellationToken cancellationToken = default);
    }
}
