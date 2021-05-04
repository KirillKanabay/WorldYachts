using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.Helpers.Generators;
using Xunit;

namespace WorldYachts.Tests.Helpers
{
    public class PasswordGeneratorTests
    {
        [Fact]
        public void CheckDifferentGeneratedPassword()
        {
            var passwordGenerator = new PasswordGenerator();
            
            var pass1 = passwordGenerator.Generate();
            var pass2 = passwordGenerator.Generate();

            Assert.NotEqual(pass1, pass2);
        }
    }
}
