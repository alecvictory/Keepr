using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.server.Models;

namespace Keepr.server.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        // NOTE This function is to get all the VaultKeeps
        internal List<VaultKeepViewModel> GetVaultKeeps(int id)
        {
            string sql = @"
                SELECT
                k.*,
                v.name,
                vk.id as vaultKeepId,
                vk.keepId as keepId,
                vk.vaultId as vaultId
                FROM
                vault_keeps vk
                JOIN vaults v ON v.id = vk.vaultId
                JOIN keeps k ON k.id = vk.keepId
                WHERE
                vk.vaultId = @id;";
            return _db.Query<VaultKeepViewModel>(sql, new { id }).ToList();
        }
        // NOTE this function is getting a VaultKeep by passing it's ID 

        internal VaultKeepViewModel GetVaultKeepById(int id)
        {
            string sql = @"
                SELECT
                k.*,
                v.name,
                vk.id as vaultKeepId,
                vk.keepId as keepId,
                vk.vaultId as vaultId
                FROM
                vault_keeps vk
                JOIN vaults v ON v.id = vk.vaultId
                JOIN keeps k ON k.id = vk.keepId
                WHERE
                vk.Id = @id;";
            return _db.Query<VaultKeepViewModel>(sql, new { id }).FirstOrDefault();
        }
        // NOTE this function is creating a new VaultKeep
        internal VaultKeep CreateVaultKeep(VaultKeep vk)
        {
            string sql = @"INSERT INTO 
            vault_keeps(vaultId, keepId, creatorId)
            VALUES (@VaultId, @KeepId, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";

            vk.Id = _db.ExecuteScalar<int>(sql, vk);
            return vk;
        }
        // NOTE this function is deleting a VaultKeep by it's ID
        internal void Remove(int id)
        {
            string sql = "DELETE FROM vault_keeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}