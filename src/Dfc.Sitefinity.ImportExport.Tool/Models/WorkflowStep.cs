using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Dfc.Sitefinity.ImportExport.Tool.Models
{
    public class WorkflowStep
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Action Action { get; set; }
        
        public string ContentType { get; set; }
        
        
        public string Directory { get; set; }
        
        public JObject Data { get; set; }
        
        public Relation[] Relates { get; set; }
    }
}