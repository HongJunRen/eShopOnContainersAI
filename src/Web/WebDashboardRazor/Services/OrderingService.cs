﻿using Microsoft.eShopOnContainers.BuildingBlocks.Resilience.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.eShopOnContainers.WebDashboardRazor.Infrastructure;
using Microsoft.eShopOnContainers.WebDashboardRazor.Models;

namespace Microsoft.eShopOnContainers.WebDashboardRazor.Services
{
    public class OrderingService : IOrderingService
    {
        private readonly AppSettings appSettings;
        private readonly IHttpClient apiClient;

        public OrderingService(IOptionsSnapshot<AppSettings> settings, IHttpClient httpClient)
        {
            this.appSettings = settings.Value;
            this.apiClient = httpClient;
        }

        public async Task<IEnumerable<ProductSales>> GetProductSalesAsync()
        {
            var dataString = await apiClient.GetStringAsync(API.Ordering.ProductStats(appSettings.WebShoppingUrl, "json"));

            return JsonConvert.DeserializeObject<IEnumerable<ProductSales>>(dataString);
        }
    }
}
