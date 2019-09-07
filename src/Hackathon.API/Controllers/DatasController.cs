using Hackathon.Application.Interfaces;
using Hackathon.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hackathon.API.Controllers
{
    [Route("Datas")]
    [ApiVersion("1.0")]
    [ApiController]
    public class DatasController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DatasController(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        [HttpGet]
        public async Task<IActionResult> GetDatas(CancellationToken cancellationToken)
        {
            var allData = await _dataService.GetAll(cancellationToken);
            return Ok(allData);
        }

        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> GetReport(CancellationToken cancellationToken)
        {
            var reportData = await _dataService.GetReportAsync(cancellationToken);
            return Ok(reportData);
        }

        [HttpGet]
        [Route("{id}/Article")]
        public async Task<IActionResult> GetArticleByData(DateTime id, CancellationToken cancellationToken)
        {
            var data = await _dataService.GetArticleByDataIdAsync(id, cancellationToken);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        [Route("{id}/Article")]
        public async Task<IActionResult> CreateArticleForData(CreateArticleRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _dataService.AddNewArticleAsync(request, cancellationToken));
        }
    }
}