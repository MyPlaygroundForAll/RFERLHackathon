using Hackathon.Domain.Exceptions;
using RFERL.Modules.Framework.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackathon.Domain.Model
{
    public class Data : DomainEntity<DateTime>, IAggregateRoot
    {
        private readonly List<Article> _articles;

        private Data(DateTime id, string temperature, string minimumTemperature, string maximumTemperature, string rainFall)
        {
            Id = id;
            Temperature = temperature ?? throw new ArgumentNullException(nameof(temperature));
            MinimumTemperature = minimumTemperature ?? throw new ArgumentNullException(nameof(minimumTemperature));
            MaximumTemperature = maximumTemperature ?? throw new ArgumentNullException(nameof(maximumTemperature));
            RainFall = rainFall ?? throw new ArgumentNullException(nameof(rainFall));
            _articles = new List<Article>();
        }

        public string Temperature { get; private set; }

        public string MinimumTemperature { get; private set; }

        public string MaximumTemperature { get; private set; }

        public string RainFall { get; private set; }

        public IReadOnlyList<Article> Articles => _articles.AsReadOnly();

        public static Data Create(DateTime id, string temperature, string minimumTemperature, string maximumTemperature, string rainFall)
        {
            return new Data(id, temperature, minimumTemperature, maximumTemperature, rainFall);
        }

        public void AddArticle(Article article)
        {
            if (_articles.Contains(article))
            {
                throw new HackathonException("Unable to add an existing article.");
            }

            _articles.Add(article);
        }

        public void RemoveArticle(int articleId)
        {
            var existingArticle = _articles.FirstOrDefault(a => a.Id == articleId);
            if (existingArticle == null)
            {
                throw new HackathonException("Unable to remove an existing article.");
            }

            _articles.Remove(existingArticle);
        }
    }
}
