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
        // NOTE this function is passed to the repository to get all VaultKeeps
        internal List<VaultKeepViewModel> GetVaultKeeps(int id)
        {
            return _vk.GetVaultKeeps(id);
        }
        // NOTE this function is passed to the repository and it is getting a vaultkeep by it's ID. If the vaultkeep does not exist it'll throw an error.
        internal VaultKeepViewModel GetVaultKeepById(int id)
        {
            VaultKeepViewModel v = _vk.GetVaultKeepById(id);
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
        // NOTE this function is passed to the repository to create a vaultkeep
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