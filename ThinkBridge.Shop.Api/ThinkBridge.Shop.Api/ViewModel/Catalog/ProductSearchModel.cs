using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridge.Shop.Api.ViewModel
{
    public partial class ProductSearchModel 
    {
        #region Ctor

        public ProductSearchModel()
        {
            AvailableCategories = new List<GenericDropDownItem>();
            AvailableManufacturers = new List<GenericDropDownItem>();
            Page = 1;
            PageSize = 10;
        }

        #endregion

        #region Properties
        public string Keyword { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchProductName { get; set; }
        public int SearchCategoryId { get; set; }
        public int SearchManufacturerId { get; set; }
        public IList<GenericDropDownItem> AvailableCategories { get; set; }

        public IList<GenericDropDownItem> AvailableManufacturers { get; set; }


        #endregion
    }
    public class GenericDropDownItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PicUrl { get; set; }
    }
}
