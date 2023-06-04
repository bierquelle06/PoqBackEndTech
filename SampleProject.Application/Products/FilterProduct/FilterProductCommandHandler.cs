using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Dapper;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;

namespace SampleProject.Application.Products.FilterProduct
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class FilterProductCommandHandler : ICommandHandler<FilterProductCommand, ProductFilterDto>
    {
        private readonly HttpClient _httpClient;
        private const string _productUrl = "http://www.mocky.io/v2/5e307edf3200005d00858b49";


        internal FilterProductCommandHandler(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<ProductFilterDto> Handle(FilterProductCommand request, CancellationToken cancellationToken)
        {
            var products = (await GetProductsFromUrl()).Products;

            if (products == null) 
                return null;

            if (request.MinPrice.HasValue)
            {
                products = products.Where(p => p.Price >= request.MinPrice);
            }

            if (request.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= request.MaxPrice);
            }

            if (!string.IsNullOrEmpty(request.Size))
            {
                products = products.Where(p => p.Sizes.Contains(request.Size));
            }

            if (!string.IsNullOrEmpty(request.Highlight))
            {
                products = products.Select(p =>
                {
                    p.Description = HighlightDescription(p.Description, request.Highlight);
                    return p;
                });
            }

            if (products.Count() == 0)
                return new ProductFilterDto();

            var result = new ProductFilterDto()
            {
                MinPrice = products.Min(p => p.Price),
                MaxPrice = products.Max(p => p.Price),
                Sizes = products.SelectMany(p => p.Sizes).Distinct(),
                CommonWords = GetMostCommonWords(products), 
                Product = products
            };

            return result;
        }

        private IEnumerable<string> GetMostCommonWords(IEnumerable<ProductDto> products)
        {
            var wordCount = new Dictionary<string, int>();

            foreach (var product in products)
            {
                var words = product.Description.Split(' ');

                foreach (var word in words)
                {
                    if (!wordCount.ContainsKey(word))
                    {
                        wordCount[word] = 0;
                    }

                    wordCount[word]++;
                }
            }

            return wordCount
                .OrderByDescending(kv => kv.Value)
                .Skip(5) // Exclude the most common five words
                .Take(10) // Take the top ten common words
                .Select(kv => kv.Key)
                .ToArray();
        }

        private string HighlightDescription(string description, string highlight)
        {
            if (!highlight.Contains(","))
                return description;

            var wordsToHighlight = highlight.Split(',');

            foreach (var word in wordsToHighlight)
            {
                description = description.Replace(word, $"<em>{word}</em>");
            }

            return description;
        }

        private class RootObject
        {
            public IEnumerable<ProductDto> Products { get; set; }
            public ApiKeys ApiKeys { get; set; }
        }

        private class ApiKeys
        {
            public string Primary { get; set; }
            public string Secondary { get; set; }
        }

        private async Task<RootObject> GetProductsFromUrl()
        {
            var response = await this._httpClient.GetAsync(_productUrl);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<RootObject>(content);
        }
    }
}