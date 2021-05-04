using System;
using System.Collections.Generic;
using System.Text;
using WorldYachts.DependencyInjections.Helpers;
using WorldYachts.Helpers.Generators;
using Xunit;

namespace WorldYachts.Tests.Helpers
{
    public class TranslitGeneratorTests
    {
        [Theory]
        [InlineData("Кирилл", "kirill")]
        [InlineData("Канабай", "kanabay")]
        [InlineData("Владимир", "vladimir")]
        public void TranslitTest(string value, string expected)
        {
            ITranslitGenerator tg = new TranslitGenerator();

            var result = tg.Transform(value);

            Assert.Equal(expected, result);
        }

    }
}
