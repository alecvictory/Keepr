using System;
using System.Collections.Generic;
using Keepr.server.Models;
using Keepr.server.Repositories;

namespace Keepr.server.Services
{
    public class VaultsService
    {

        private readonly VaultsRepository _vr;

        public VaultsService(VaultsRepository vr)
        {
            _vr = vr;
        }
        // NOTE this function is passed to the repository to get all vaults
        internal List<Vault> GetAllVaults()
        {
            return _vr.GetAllVaults();
        }
        // NOTE this function is passed to the repository and it is getting a vault by it's ID. If the vault does not exist it'll throw an error.
        internal Vault GetVaultById(int id)
        {
            var v = _vr.GetVaultById(id);
            if (v == null)
            {
                throw new Exception("Invalid Id");
            }

            return v;
        }
        // NOTE this function is passed to the repository to create a vault
        internal Vault CreateVault(Vault v)
        {
            return _vr.CreateVault(v);
        }
        // NOTE this function is passed to the repository to edit a vault. The function will get the vault by an ID if the vault does not exist it'll throw an error. It will check if the creator ID matches the user ID, if it doesn't it'll throw an error. 
        internal Vault UpdateVault(Vault v, string userId)
        {
            Vault vault = _vr.GetVaultById(v.Id);

            if (vault == null)
            {
                throw new Exception("Invalid Id");
            }
            if (vault.CreatorId != userId)
            {
                throw new Exception("This is not your Vault to edit");
            }
            return _vr.UpdateVault(v);
        }
        // NOTE this function is passed to the repository to delete a vault. It will first get the vault by it's Id then it will check if the creator ID matches the user ID. If it does not then it will throw an error.
        internal void RemoveVault(int id, string userId)
        {
            Vault vault = GetVaultById(id);

            if (vault.CreatorId != userId)
            {
                throw new Exception("You are not the owner of this Vault");
            }
            _vr.Remove(id);
        }
        // NOTE this function is passed to the repository to get all vaults by profile Id
        internal List<Vault> GetVaultsByProfileId(string id, string userInfoId)
        {
            if (id == userInfoId)
            {
                return _vr.GetVaultsByProfileId(userInfoId);
            }
            return _vr.GetVaultsByProfileId(id);
        }
    }
}