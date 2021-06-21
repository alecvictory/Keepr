using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.server.Models;

namespace Keepr.server.Repositories
{
    public class KeepsRepository
    {

        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        // NOTE This function is to get all the Keeps
        internal List<Keep> GetAllKeeps()
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId;";
            return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }, splitOn: "id").ToList();
        }
        // NOTE this function is getting a keep by passing it's ID 
        internal Keep GetKeepById(int id)
        {
            string sql = @"
            SELECT 
                k.*,
                a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.id = @id";
            return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }, new { id }).FirstOrDefault();
        }
        // NOTE this function is creating a new keep

        internal Keep CreateKeep(Keep k)
        {
            string sql = @"
            INSERT INTO
            keeps(name, description, img, views, shares, keeps, creatorId)
            VALUES (@Name, @Description, @Img, 0, 0, 0, @Creatorid);
            SELECT LAST_INSERT_ID();";
            k.Id = _db.ExecuteScalar<int>(sql, k);
            return k;
        }
        // NOTE this function is getting keeps by a profile ID by passing the profile ID
        internal List<Keep> GetKeepsByProfileId(string id)
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.creatorId = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
            {
                k.Creator = p;
                return k;
            }, new { id }).ToList();
        }
        // NOTE this function is editing a Keep
        internal Keep UpdateKeep(Keep k)
        {
            string sql = @"
            UPDATE keeps
            SET
                name = @Name,
                description = @Description,
                img = @Img
            WHERE id = @Id;";
            _db.Execute(sql, k);
            return k;
        }
        // NOTE this function is deleting a Keep by passing it's ID
        internal void Remove(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}