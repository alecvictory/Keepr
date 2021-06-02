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

        internal List<Vault> GetAllVaults()
        {
            return _vr.GetAllVaults();
        }

        internal Vault GetVaultById(int id)
        {
            var v = _vr.GetVaultById(id);
            if (v == null)
            {
                throw new Exception("Invalid Id");
            }

            return v;
        }

        internal Vault CreateVault(Vault v)
        {
            return _vr.CreateVault(v);
        }

        internal Vault UpdateVault(Vault v, string id)
        {
            Vault vault = _vr.GetVaultById(v.Id);

            if (vault == null)
            {
                throw new Exception("Invalid Id");
            }
            if (vault.CreatorId != id)
            {
                throw new Exception("This is not your Vault to edit");
            }
            return _vr.UpdateVault(v);
        }

        internal void RemoveVault(int id, string userId)
        {
            Vault vault = GetVaultById(id);

            if (vault.CreatorId != userId)
            {
                throw new Exception("You are not the owner of this Vault");
            }
            _vr.Remove(id);
        }

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