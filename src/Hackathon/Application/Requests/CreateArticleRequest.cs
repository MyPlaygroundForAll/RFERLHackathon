using Microsoft.AspNetCore.Http;
using RFERL.Modules.Framework.Common.Interfaces;
using System;

namespace Hackathon.Application.Requests
{
    public class CreateArticleRequest : IRequestHasIdentity<DateTime>
    {
        public DateTime Id { get; set; }

        public IFormFile Image { get; set; }

        public string Body { get; set; }
    }
}
