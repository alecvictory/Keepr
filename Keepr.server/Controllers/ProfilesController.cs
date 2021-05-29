using Keepr.server.Models;
using Keepr.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {

        private readonly AccountsService _accountsService;

        public ProfilesController(AccountsService accountService)
        {
            _accountsService = accountService;
        }


        [HttpGet("{id}")]
        public ActionResult<Profile> GetProfileById(string id)
        {
            try
            {
                Profile p = _accountsService.GetProfileById(id);
                return Ok(p);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}