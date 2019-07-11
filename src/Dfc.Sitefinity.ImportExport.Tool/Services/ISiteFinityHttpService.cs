using System.Collections.Generic;
using System.Threading.Tasks;
using Dfc.Sitefinity.ImportExport.Tool.Models;
using Microsoft.Extensions.Logging;

namespace Dfc.Sitefinity.ImportExport.Tool.Services
{
    public interface ISiteFinityHttpService
    {
        Task<T> Get<T>(string contentType) where T : class;
        Task<List<T>> GetAll<T>(string contentType) where T : class;
        Task<List<TaxonomyHierarchy>> GetTaxonomyInstances(string taxonomyName);
        Task<string> PostData(string contentType, object data);
        Task<string> PostData(string contentType, string data);
        Task Delete(string contentType);
        string MakeAbsoluteUri(string relativePortion);
        ILogger Logger { get; set; }
        Task<string> GetLatestIndex(string key);
    }
}
