using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Store
{
    public record TriggerState
    {
        public int TriggerCount { get; init; }
    }

    public class TriggerFeatureState : Feature<TriggerState>
    {
        public override string GetName() => nameof(TriggerState);
      
        protected override TriggerState GetInitialState()
        {
            return new TriggerState { TriggerCount = 0 };
        }
    }
}
