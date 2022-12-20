using System;
using System.Collections.Generic;
using System.Text;

namespace LevelUpCSharp.Storage
{
    public interface IVault<T>
    {
        void Put(T item);

        T Get();
    }
}
