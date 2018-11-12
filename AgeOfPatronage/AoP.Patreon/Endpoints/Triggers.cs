using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgeOfPatronage.Patreon.Endpoints
{
    class Triggers
    {
        
        /// <summary>
        /// Triggered when a new member is created. Note that you may get more than one of these per patron if they delete and renew their membership. 
        /// Member creation only occurs if there was no prior payment between patron and creator.
        /// </summary>
        /// <returns></returns>
        public Task CreateMembersTrigger()
        {
            //members:create
            return Task.CompletedTask;
        }
        /// <summary>
        /// Triggered when the membership information is changed. Includes updates on payment charging events
        /// </summary>
        /// <returns></returns>
        public Task UpdateMembersTrigger()
        {
            //members:update
            return Task.CompletedTask;
        }
        /// <summary>
        /// Triggered when a membership is deleted. Note that you may get more than one of these per patron if they delete and renew their membership. 
        /// Deletion only occurs if no prior payment happened, otherwise pledge deletion is an update to member status.
        /// </summary>
        /// <returns></returns>
        public Task DeleteMembersTrigger()
        {
            //members:delete
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when a new pledge is created for a member. 
        /// This includes when a member is created through pledging, and when a follower becomes a patron.
        /// </summary>
        /// <returns></returns>
        public Task CreateMembersPledgeTrigger()
        {
            //members:pledge:create
            return Task.CompletedTask;
        }
        /// <summary>
        /// Triggered when a member updates their pledge.
        /// </summary>
        /// <returns></returns>
        public Task UpdateMembersPledgeTrigger()
        {
            //members:pledge:update
            return Task.CompletedTask;
        }
        /// <summary>
        /// Triggered when a member deletes their pledge.
        /// </summary>
        /// <returns></returns>
        public Task DeleteMembersPledgeTrigger()
        {
            //members:pledge:delete
            return Task.CompletedTask;
        }
    }
}
