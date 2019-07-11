using System;
using Newtonsoft.Json;

namespace Dfc.Sitefinity.ImportExport.Tool.Models
{
    public class Relation
    {
        public RelationType RelatedType { get; set; }
        
        
        public string[] Values { get; set; }

        public string Property => RelatedType.Property;
        
        public string ContentType => RelatedType.ContentType;
        
        [JsonIgnore]
        public bool IsTaxonomy => RelatedType.Type.Equals("taxonomies", StringComparison.InvariantCultureIgnoreCase);
        

    }
}