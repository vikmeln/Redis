using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Redis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public RedisController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save(string name)
        {
            await _cache.SetStringAsync(
                "userName",
                name
            );
            return Ok($"Имя {name} сохранено в Redis");
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var name = await _cache.GetStringAsync("userName");

            if (name == null)
            {
                return NotFound("Данных в Redis нет");
            }
            return Ok($"Имя из Redis: {name}");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete()
        {
            await _cache.RemoveAsync("userName");
            return Ok("Данные удалены из Redis");
        }
    }
}