using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess.Mechanics
{
    public enum GlowType
    {
        Selected,
        CanMove,
        Stop
    }

    public enum SoundType
    {
        Move,
        Take,
        Check
    }

    public enum ConsequenceType
    {
        None,
        QueenGrows
    }
}
