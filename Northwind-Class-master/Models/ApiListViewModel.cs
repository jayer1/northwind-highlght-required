using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Northwind.Models.ViewModels
{
    public class ApiListViewModel
    {
        public IEnumerable<ApiViewOrder> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class ApiViewOrder
    {
        [JsonProperty(PropertyName = "id")]
        public int OrderID { get; set; }
        [JsonProperty(PropertyName = "cust")]
        public string ShipName { get; set; }
        [JsonProperty(PropertyName = "order")]
        public DateTime OrderDate { get; set; }
        [JsonProperty(PropertyName = "required")]
        public DateTime RequiredDate { get; set; }
        [JsonProperty(PropertyName = "shipped")]
        public DateTime ShippedDate { get; set; }
    }
}
