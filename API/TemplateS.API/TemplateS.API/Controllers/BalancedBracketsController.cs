using Microsoft.AspNetCore.Mvc;
using TemplateS.Application.Interfaces;

namespace TemplateS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancedBracketsController : ControllerBase
    {
        private readonly IBalancedBracketService _balancedBracketService;

        public BalancedBracketsController(IBalancedBracketService balancedBracketService)
        {
            _balancedBracketService = balancedBracketService;
        }

        [HttpPut("{balancedBracket}")]
        public IActionResult Post(string balancedBracket) => Ok(_balancedBracketService.Verify(balancedBracket));
    }
}
