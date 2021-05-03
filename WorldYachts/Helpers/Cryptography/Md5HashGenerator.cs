using System;
using System.Security.Cryptography;
using System.Text;

namespace WorldYachts.Helpers.Cryptography
{
    public class Md5HashCalculator:IHashCalculator
    {
        public string GetHash(string value)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            return Convert.ToBase64String(hash);
        }
    }
}
