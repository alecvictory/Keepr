using System;
using System.Collections.Generic;
using Keepr.server.Models;
using Keepr.server.Repositories;

namespace Keepr.server.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vr;

        public VaultKeepsService(VaultKeepsRepository vr)
        {
            _vr = vr;
        }

        internal List<VaultKeep> GetVaultKeeps(int id)
        {
            return _vr.GetVaultKeeps(id);
        }
        internal VaultKeep GetVaultKeepById(int id, VaultKeep vk)
        {
            var v = _vr.GetVaultKeepById(id);
            if (v == null)
            {
                throw new Exception("Invalid Id");
            }
            return v;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vk)
        {
            return _vr.CreateVaultKeep(vk);
        }

        internal void RemoveVaultKeep(int id, string userId, VaultKeep vk)
        {
            VaultKeep vaultKeep = GetVaultKeepById(id, vk);

            if (vaultKeep.CreatorId != userId)
            {
                throw new Exception("You are not the owner of this");
            }
            _vr.Remove(id);
        }
    }
}