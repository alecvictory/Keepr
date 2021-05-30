using System;
using System.Collections.Generic;
using Keepr.server.Models;
using Keepr.server.Repositories;

namespace Keepr.server.Services
{
    public class VaultsService
    {

        private readonly VaultsRepository _vp;

        public VaultsService(VaultsRepository vp)
        {
            _vp = vp;
        }

        internal List<Vault> GetAllVaults()
        {
            return _vp.GetAllVaults();
        }

        internal Vault GetVaultById(int id)
        {
            var v = _vp.GetVaultById(id);
            if (v == null)
            {
                throw new Exception("Invalid Id");
            }

            return v;
        }

        internal Vault CreateVault(Vault v)
        {
            return _vp.CreateVault(v);
        }

        internal Vault UpdateVault(Vault v, string id)
        {
            Vault vault = _vp.GetVaultById(v.Id);

            if (vault == null)
            {
                throw new Exception("Invalid Id");
            }
            if (vault.CreatorId != id)
            {
                throw new Exception("This is not your Vault to edit");
            }
            return _vp.UpdateVault(v);
        }

        internal void RemoveVault(int id, string userId)
        {
            Vault vault = GetVaultById(id);

            if (vault.CreatorId != userId)
            {
                throw new Exception("You are not the owner of this Vault");
            }
            _vp.Remove(id);
        }

        internal List<Vault> GetVaultsByProfileId(string id, string userInfoId)
        {
            if (id == userInfoId)
            {
                return _vp.GetVaultsByProfileId(userInfoId);
            }
            return _vp.GetVaultsByProfileId(id);
        }
    }
}