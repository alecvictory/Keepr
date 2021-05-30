using System;
using System.Collections.Generic;
using Keepr.server.Models;
using Keepr.server.Repositories;

namespace Keepr.server.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vk;

        public VaultKeepsService(VaultKeepsRepository vk)
        {
            _vk = vk;
        }

        internal List<VaultKeepViewModel> GetVaultKeeps(int id)
        {
            return _vk.GetVaultKeeps(id);
        }
        internal VaultKeepViewModel GetVaultKeepById(int id, VaultKeep vk)
        {
            var v = _vk.GetVaultKeepById(id);
            if (v == null)
            {
                throw new Exception("Invalid Id");
            }
            return v;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vk)
        {
            return _vk.CreateVaultKeep(vk);
        }

        internal void RemoveVaultKeep(int id, string creatorId)
        {
            VaultKeepViewModel vk = _vk.GetVaultKeepById(id);
            if (vk.CreatorId != creatorId)
            {
                throw new Exception("You are not the owner of this");
            }
            _vk.Remove(id);
        }
    }
}