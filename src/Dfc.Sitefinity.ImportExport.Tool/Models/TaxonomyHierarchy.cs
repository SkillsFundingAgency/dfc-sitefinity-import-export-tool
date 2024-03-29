﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Dfc.Sitefinity.ImportExport.Tool.Models
{
    [ExcludeFromCodeCoverage]
    public class TaxonomyHierarchy
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlName { get; set; }
        public Guid TaxonomyId { get; set; }
    }
}
