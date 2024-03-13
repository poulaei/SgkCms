using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CmsKitDemo.Entities;

public interface IBoxRepository : IBasicRepository<Box, Guid>
{
    Task<List<Box>> GetListAsync(
        string filter = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        bool includeDetails=false,
        CancellationToken cancellationToken = default
        );

    Task<long> GetCountAsync(
        string filter = null,
        CancellationToken cancellationToken = default
        );

    Task<Box> GetBySectionAsync(string section, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> SectionExistsAsync(string section, CancellationToken cancellationToken = default);
}
