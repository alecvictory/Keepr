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

        internal List<VaultKeep> GetVaultKeeps(int id)
        {
            string sql = @"
                SELECT
                vk.id as vaultKeepId,
                vk.keepId as KeepId,
                vk.vaultId as vaultId
                FROM
                vault_keeps vk
                JOIN vaults v ON v.id = vk.vaultId
                JOIN keeps k ON k.id = vk.keepId
                WHERE
                vk.vaultId = @id;
            ";
            return _db.Query<VaultKeep>(sql, new { id }).ToList();
        }

        internal VaultKeep GetVaultKeepById(int id)
        {
            throw new NotImplementedException();
        }

        // internal VaultKeep GetVaultKeepById(int id, VaultKeep vk)
        // {
        //     string sql = @"
        //     SELECT
        //       vk.*
        //       a.*  
        //     FROM vault_keeps vk
        //     JOIN accounts a ON a.id = vk.creatorId
        //     WHERE vk.id = @id";
        //     return _db.Query<VaultKeep, Account, VaultKeep>(sql, (vk, a) =>
        //     {
        //         vk.CreatorId = a;
        //         return vk;
        //     }, new { id }.FirstOrDefault());
        // }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM vault_keeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vk)
        {
            string sql = @"INSERT INTO 
            vault_keeps(vaultId, keepId)
            VALUES (@VaultId, @KeepId);
            SELECT LAST_INSERT_ID();
            ";

            vk.Id = _db.ExecuteScalar<int>(sql, vk);
            return vk;
        }
    }
}