using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Dfc.Sitefinity.ImportExport.Tool.Models
{
    [ExcludeFromCodeCoverage]
    public class SiteFinityDataFeed<T> where T : class
    {
        [JsonProperty("value")]
        public T Value { get; set; }
    }
}
