using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.DependencyInjections.Helpers
{
    public interface ITranslitGenerator
    {
        string Transform(string value);
    }
}
