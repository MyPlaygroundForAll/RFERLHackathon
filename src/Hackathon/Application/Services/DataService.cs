using Hackathon.Application.Interfaces;
using Hackathon.Application.Requests;
using Hackathon.Application.ViewModels;
using Hackathon.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon.Application.Services
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public async Task<int> AddNewArticleAsync(CreateArticleRequest request, CancellationToken cancellationToken)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "images", request.Image.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            var data = await _dataRepository.FindByIdAsync(request.Id);
            var newArticle = Article.Create(path, request.Body);
            data.AddArticle(newArticle);

            _dataRepository.Update(data);
            await _dataRepository.UnitOfWork.SaveChangesAsync();
            return newArticle.Id;
        }

        public async Task<IEnumerable<DataViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var allData = await _dataRepository.FindAsync(d => true);
            return allData.Select(d => new DataViewModel()
            {
                Timeline = d.Id,
                Temperature = d.Temperature,
                MaximumTemperature = d.MaximumTemperature,
                MinimumTemperature = d.MinimumTemperature,
                Rainfall = d.RainFall
            }).ToList();
        }

        public async Task<ArticleViewModel> GetArticleByDataIdAsync(DateTime id, CancellationToken cancellationToken)
        {
            var data = await _dataRepository.FindByIdAsync(id);
            var article = data.Articles.FirstOrDefault();
            return new ArticleViewModel()
            {
                Timeline = data.Id,
                ImageUrl = article.ImageUrl,
                Body = article.Body
            };
        }

        public async Task<IEnumerable<DataReportViewModel>> GetReportAsync(CancellationToken cancellationToken)
        {
            var allData = await _dataRepository.FindAsync(d => true);
            var timeLines = allData.Select(s => s.Id).ToList();
            var temperatures = allData.Select(s => s.Temperature).ToList();
            var minimumTemperatures = allData.Select(s => s.MinimumTemperature).ToList();
            var maximumTemperatures = allData.Select(s => s.MaximumTemperature).ToList();
            var rainfalls = allData.Select(s => s.RainFall).ToList();
            return allData.Select(s => new DataReportViewModel()
            {
                Timelines = timeLines,
                Data = new DataReportViewModel.DataReportDetail()
                {
                    Temperatures = temperatures,
                    MinimumTemperatures = minimumTemperatures,
                    MaximumTemperatures = maximumTemperatures,
                    Rainfalls = rainfalls
                }
            });
        }
    }
}
