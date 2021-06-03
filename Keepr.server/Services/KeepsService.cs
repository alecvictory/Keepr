using System;
using System.Collections.Generic;
using Keepr.server.Models;
using Keepr.server.Repositories;

namespace Keepr.server.Services
{
    public class KeepsService
    {

        private readonly KeepsRepository _kp;

        public KeepsService(KeepsRepository kp)
        {
            _kp = kp;
        }

        internal List<Keep> GetAllKeeps()
        {
            return _kp.GetAllKeeps();
        }

        internal Keep GetKeepById(int id)
        {
            var k = _kp.GetKeepById(id);
            if (k == null)
            {
                throw new Exception("Invalid Id");
            }
            k.Views++;
            k = _kp.UpdateKeep(k);
            return k;
        }

        internal Keep CreateKeep(Keep k)
        {
            return _kp.CreateKeep(k);
        }

        internal Keep UpdateKeep(Keep k, string id)
        {
            Keep keep = _kp.GetKeepById(k.Id);

            if (keep == null)
            {
                throw new Exception("Invalid Id");
            }
            if (keep.CreatorId != id)
            {
                throw new Exception("This is not your Vault to edit");
            }
            return _kp.UpdateKeep(k);
        }

        internal List<Keep> GetKeepsByProfileId(string id)
        {
            return _kp.GetKeepsByProfileId(id);
        }

        internal void RemoveKeep(int id, string userId)
        {
            Keep keep = GetKeepById(id);

            if (keep.CreatorId != userId)
            {
                throw new Exception("You are not the owner of this Keep");
            }
            _kp.Remove(id);
        }
    }
}