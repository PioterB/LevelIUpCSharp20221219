using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LevelUpCSharp.Retail
{
    internal class VendingMachine<TType, TKind> : IVendingMachine<TType, TKind>
    {
        public IEnumerator<TType> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Put(TType item)
        {
            throw new NotImplementedException();
        }

        public TType Take(TKind kind)
        {
            throw new NotImplementedException();
        }
    }
}
