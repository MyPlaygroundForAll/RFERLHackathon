using Hackathon.Application.Requests;
using Hackathon.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon.Application.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<DataViewModel>> GetAll(CancellationToken cancellationToken);

        Task<IEnumerable<DataReportViewModel>> GetReportAsync(CancellationToken cancellationToken);

        Task<ArticleViewModel> GetArticleByDataIdAsync(DateTime id, CancellationToken cancellationToken);

        Task<int> AddNewArticleAsync(CreateArticleRequest request, CancellationToken cancellationToken);
    }
}
