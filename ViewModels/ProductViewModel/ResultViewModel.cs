using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.ViewModels.ProductViewModel
{
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
        
    }
}
