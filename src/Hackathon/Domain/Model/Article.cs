using RFERL.Modules.Framework.Data.Abstractions;
using System;

namespace Hackathon.Domain.Model
{
    public class Article : DomainEntity<int>
    {
        private Article(string imageUrl, string body)
        {
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string ImageUrl { get; private set; }

        public string Body { get; private set; }

        public static Article Create(string imageUrl, string body)
        {
            return new Article(imageUrl, body);
        }

        public void Update(string imageUrl, string body)
        {
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }
    }
}
