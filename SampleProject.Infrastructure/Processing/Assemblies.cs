using System.Reflection;
using SampleProject.Application.Products.FilterProduct;

namespace SampleProject.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(FilterProductCommand).Assembly;
    }
}