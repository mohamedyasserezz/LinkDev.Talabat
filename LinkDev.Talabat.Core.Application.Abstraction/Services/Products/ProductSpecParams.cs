using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Products
{
    public class ProductSpecParams
    {
        private int _pageSize = 5;
        private const int MaxPageSize = 10;
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public string? sort { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > MaxPageSize ? MaxPageSize : value);
            }
        }
    }
}
