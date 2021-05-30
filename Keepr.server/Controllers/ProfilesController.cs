using System.Collections.Generic;
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
        private readonly KeepsService _ks;

        public ProfilesController(AccountsService accountService, KeepsService ks)
        {
            _accountsService = accountService;
            _ks = ks;
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

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfileId(string id)
        {
            try
            {
                List<Keep> keeps = _ks.GetKeepsByProfileId(id);
                return Ok(keeps);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}