using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;

namespace DodgeRoll
{
    internal class DodgeRollEntityBehaviour : EntityBehavior
    {

        private long _keypressed;
        private ILogger _logger;

        public DodgeRollEntityBehaviour(Entity entity) : base(entity){}
        public override void Initialize(EntityProperties properties, JsonObject attributes)
        {
            base.Initialize(properties, attributes);
            _logger = entity.Api.Logger;
            var api = (entity as EntityAgent).Api;
            if( api is ICoreClientAPI clientApi){
                clientApi.Input.InWorldAction +=_sprintEvent;
            }

        }

        private void _sprintEvent(EnumEntityAction action, bool on, ref EnumHandling handled)
        {
            
            if(action == EnumEntityAction.Sprint){
            long elapsedMilliseconds = entity.Api.World.ElapsedMilliseconds;
                if(on){
                    _keypressed=elapsedMilliseconds;
                }
                else if(elapsedMilliseconds<_keypressed + 500 ){
                    _logger.Debug("dodge du motherfker");
                    // TODO stop running
                    
                }
                else{
                    _logger.Debug("YEEEEEEEEEEEEEEEEET");
                }

            }
        }

        public override string PropertyName()
        {
            return "behaviourDodgeRoll";
        }
    }
}