using WorldYachts.DependencyInjections.Helpers;

namespace WorldYachts.Helpers.Generators
{
    public class PasswordGenerator:IPasswordGenerator
    {
        public string Generate()
        {
            string generatedPassword = SPG.lib.SPG.Generate(new[] {"en", "En", "num"}, 16);
            return generatedPassword;
        }
    }
}
