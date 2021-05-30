using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
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
        private readonly VaultsService _vs;

        public ProfilesController(AccountsService accountService, KeepsService ks, VaultsService vs)
        {
            _accountsService = accountService;
            _ks = ks;
            _vs = vs;
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

        [HttpGet("{id}/vaults")]

        public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string id)
        {
            try
            {
                Account userinfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> vaults = _vs.GetVaultsByProfileId(id, userinfo.Id);
                return Ok(vaults);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}