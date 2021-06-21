using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.server.Models;

namespace Keepr.server.Repositories
{
    public class VaultsRepository
    {

        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        // NOTE This function is to get all the vaults
        internal List<Vault> GetAllVaults()
        {
            string sql = @"
            SELECT
            v.*
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id;";
            return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, splitOn: "id").ToList();
        }
        // NOTE this function is getting a vault by passing it's ID 
        internal Vault GetVaultById(int id)
        {
            string sql = @"
            SELECT 
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.id = @id";
            return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, new { id }).FirstOrDefault();

        }
        // NOTE this function is creating a new vault
        internal Vault CreateVault(Vault v)
        {
            string sql = @"
            INSERT INTO
            vaults(name, description, isPrivate, creatorId)
            VALUES (@Name, @Description, @IsPrivate, @creatorId);
            SELECT LAST_INSERT_ID();";
            v.Id = _db.ExecuteScalar<int>(sql, v);
            return v;
        }
        // NOTE this function is editing a vault
        internal Vault UpdateVault(Vault v)
        {
            string sql = @"
            UPDATE vaults
            SET
                name = @Name,
                description = @Description,
                isPrivate = @IsPrivate
            WHERE id = @Id;";
            _db.Execute(sql, v);
            return v;
        }
        // NOTE This function is getting all the vaults by profile by passing a profile ID
        internal List<Vault> GetVaultsByProfileId(string id)
        {
            string sql = @"
            SELECT 
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.creatorId = @id AND isPrivate = 0;";
            return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
            {
                v.Creator = p;
                return v;
            }, new { id }).ToList();
        }
        // NOTE This function is deleting a vault by passing it's ID
        internal void Remove(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}