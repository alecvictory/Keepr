using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.server.Models;
using Keepr.server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vk;

        public VaultKeepsController(VaultKeepsService vk)
        {
            _vk = vk;
        }

        // CREATE VAULTKEEP

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vk)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vk.CreatorId = userInfo.Id;
                VaultKeep newVK = _vk.CreateVaultKeep(vk);
                return Ok(newVK);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE VAULTKEEP

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> RemoveVaultKeep(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _vk.RemoveVaultKeep(id, userInfo?.Id);
                return Ok("Successfully Deleted");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}