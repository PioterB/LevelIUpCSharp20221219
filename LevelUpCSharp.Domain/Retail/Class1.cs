using System;
using System.Collections.Generic;
using System.Text;

namespace LevelUpCSharp.Retail
{
    public interface IVendingMachine<TType, in TKind>: IEnumerable<TType>
    {
        void Put(TType item);

        TType Take(TKind kind);
    }
}
