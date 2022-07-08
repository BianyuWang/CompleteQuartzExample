using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Store
{
    public static class TriggerCountReducer
    {
        [ReducerMethod]
        public static TriggerState OnAddTriggerCount(TriggerState state, AddTriggerCounter Action)
        {
            return state with { TriggerCount = state.TriggerCount + 1 };
        
        }
    }
}
