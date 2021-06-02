using System;
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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vk;

        public VaultsController(VaultsService vs, VaultKeepsService vk)
        {
            _vs = vs;
            _vk = vk;
        }

        // GET ALL VAULTS

        [HttpGet]

        public ActionResult<List<Vault>> GetAllVaults()
        {
            try
            {
                List<Vault> vaults = _vs.GetAllVaults();
                return Ok(vaults);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET VAULT BY VAULT ID

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetVaultById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.GetVaultById(id);
                if (userInfo == null && vault.IsPrivate == true)
                {
                    throw new Exception("This is a private vault");
                }
                if (vault.IsPrivate == true)
                {
                    if (userInfo.Id == vault.CreatorId)
                    {
                        return vault;
                    }
                    throw new Exception("This is a private vault");
                }
                return Ok(vault);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET VAULTKEEP

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<VaultKeepViewModel>>> GetVaultKeeps(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.GetVaultById(id);
                List<VaultKeepViewModel> vaultkeeps = _vk.GetVaultKeeps(id);
                if (userInfo == null && vault.IsPrivate == true)
                {
                    throw new Exception("You can't access a private vault");
                }
                if (vault.IsPrivate == true)
                {
                    if (userInfo.Id == vault.CreatorId)
                    {
                        return Ok(vaultkeeps);
                    }
                    throw new Exception("You can't access a private vault");
                }
                return Ok(vaultkeeps);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // CREATE VAULT

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault v)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                v.CreatorId = userInfo.Id;
                Vault newV = _vs.CreateVault(v);
                newV.Creator = userInfo;
                return Ok(newV);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // EDIT VAULT BY VAULT ID

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Vault>> UpdateVault(int id, [FromBody] Vault v)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                v.Id = id;
                Vault newV = _vs.UpdateVault(v, userInfo.Id);
                newV.Creator = userInfo;
                return Ok(newV);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE VAULT BY VAULT ID

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> RemoveVault(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _vs.RemoveVault(id, userInfo.Id);
                return Ok("Successfully Deleted");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}