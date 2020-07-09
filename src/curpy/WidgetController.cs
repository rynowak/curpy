using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace curpy
{
    [ApiController]
    public class WidgetController : ControllerBase
    {
        readonly ILogger logger;

        public WidgetController(ILogger<WidgetController> logger)
        {
            this.logger = logger;
        }
        
        [HttpGet("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.CustomProviders/resourceProviders/{resourceProviderName}/myCustomResource")]
        public ActionResult<List<Widget>> List(string subscriptionId, string resourceGroupName, string resourceProviderName)
        {
            return Ok(new List<Widget>()
            {
                new Widget(),
            });
        }

        [HttpGet("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.CustomProviders/resourceProviders/{resourceProviderName}/myCustomResource/{resourceName}")]
        public ActionResult<Widget> Get(string subscriptionId, string resourceGroupName, string resourceProviderName, string resourceName)
        {
            return Ok(new Widget());
        }

        [HttpPut("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.CustomProviders/resourceProviders/{resourceProviderName}/myCustomResource/{resourceName}")]
        public ActionResult<Widget> Create(string subscriptionId, string resourceGroupName, string resourceProviderName, string resourceName)
        {
            return Ok(new Widget());
        }

        [HttpDelete("/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.CustomProviders/resourceProviders/{resourceProviderName}/myCustomResource/{resourceName}")]
        public ActionResult Delete(string subscriptionId, string resourceGroupName, string resourceProviderName, string resourceName)
        {
            return NoContent();
        }

        public class Widget
        {
        }
    }
}
