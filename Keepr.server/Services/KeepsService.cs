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
        // NOTE this function is passed to the repository to get all keeps 
        internal List<Keep> GetAllKeeps()
        {
            return _kp.GetAllKeeps();
        }
        // NOTE this function is passed to the repository and it is getting a keep by it's ID. If the keep does not exist it'll throw an error. If the keep does exist it will increment the keep views automatically and update the keep.
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
        // NOTE this function is passed to the repository to create a keep
        internal Keep CreateKeep(Keep k)
        {
            return _kp.CreateKeep(k);
        }
        // NOTE this function is passed to the repository to edit a keep. The function will get the keep by an ID if the keep does not exist it'll throw an error. It will check if the creator ID matches the user ID, if it doesn't it'll throw an error. 
        internal Keep UpdateKeep(Keep k, string userId)
        {
            Keep keep = _kp.GetKeepById(k.Id);

            if (keep == null)
            {
                throw new Exception("Invalid Id");
            }
            if (keep.CreatorId != userId)
            {
                throw new Exception("This is not your Vault to edit");
            }
            return _kp.UpdateKeep(k);
        }
        // NOTE this function is passed to the repository to get all keeps by profile Id
        internal List<Keep> GetKeepsByProfileId(string id)
        {
            return _kp.GetKeepsByProfileId(id);
        }
        // NOTE this function is passed to the repository to delete a keep. It will first get the keep by it's Id then it will check if the creator ID matches the user ID. If it does not then it will throw an error.
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