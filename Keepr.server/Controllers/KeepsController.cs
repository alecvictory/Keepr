using System.Collections.Generic;
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
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        // GET ALL KEEPS

        [HttpGet]
        public ActionResult<List<Keep>> GetAllKeeps()
        {
            try
            {
                List<Keep> keeps = _ks.GetAllKeeps();
                return Ok(keeps);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET KEEP BY KEEP ID

        [HttpGet("{id}")]
        public ActionResult<Keep> GetKeepById(int id)
        {
            try
            {
                Keep keep = _ks.GetKeepById(id);
                return Ok(keep);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // CREATE KEEP

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep k)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                k.CreatorId = userInfo.Id;
                Keep newK = _ks.CreateKeep(k);
                newK.Creator = userInfo;
                return Ok(newK);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // EDIT KEEP BY KEEP ID

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Keep>> UpdateKeep(int id, [FromBody] Keep k)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                k.Id = id;
                Keep newK = _ks.UpdateKeep(k, userInfo.Id);
                newK.Creator = userInfo;
                return Ok(newK);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE KEEP BY KEEP ID

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> RemoveKeep(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _ks.RemoveKeep(id, userInfo.Id);
                return Ok("Successfully Deleted");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}