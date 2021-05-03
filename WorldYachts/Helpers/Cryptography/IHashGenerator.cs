using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Helpers.Cryptography
{
    public interface IHashCalculator
    {
        string GetHash(string value);
    }
}
