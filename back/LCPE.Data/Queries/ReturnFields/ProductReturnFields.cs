﻿namespace LCPE.Data.Queries.ReturnFields
{
    public class ProductReturnFields : BaseMetadataReturnFields
    {
        public bool Year { get; set; }
        
        public bool ProductName { get; set; }
        
        public bool ShortName { get; set; }

        public bool Organization { get; set; }

        public bool OriginCountry { get; set; }

        public bool Amount { get; set; }

        public bool Unit { get; set; }

        public bool Cost { get; set; }

        public bool Currency { get; set; }
    }
}