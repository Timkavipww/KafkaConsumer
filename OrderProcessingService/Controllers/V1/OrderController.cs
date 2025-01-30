using Microsoft.AspNetCore.Mvc;

namespace OrderProcessingService.Controllers.V1;

public class OrderController :BaseApiController
{

    [HttpPost]
    public async Task<ActionResult> AddOrder() {
        await Task.Delay(1);
        var result = "result";
        return Ok(result);
    }
}
