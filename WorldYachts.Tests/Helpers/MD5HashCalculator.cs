using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.Helpers.Cryptography;
using Xunit;

namespace WorldYachts.Tests.Helpers
{
    public class MD5HashCalculator
    {
        [Fact]
        public void CheckDifferentHash()
        {
            IHashCalculator hc = new Md5HashCalculator();
            
            var hash1 = hc.GetHash("abc");
            var hash2 = hc.GetHash("Abc");

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void CheckEqualHash()
        {
            IHashCalculator hc = new Md5HashCalculator();

            var hash1 = hc.GetHash("abc");
            var hash2 = hc.GetHash("abc");

            Assert.Equal(hash1, hash2);
        }
    }
}
