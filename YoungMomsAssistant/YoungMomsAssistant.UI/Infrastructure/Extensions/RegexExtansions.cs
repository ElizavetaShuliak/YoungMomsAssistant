using System.Text.RegularExpressions;
using YoungMomsAssistant.UI.Constants;

namespace YoungMomsAssistant.UI.Infrastructure.Extensions {
    public static class RegexExtansions {

        public static bool IsMatchEmail(string input) {
            return input != null && Regex.IsMatch(input, RegexConstants.Email);
        }

        public static bool IsMatchLogin(string input) {
            return input != null && Regex.IsMatch(input, RegexConstants.Login);
        }

        public static bool IsMatchPassword(string input) {
            return input != null && Regex.IsMatch(input, RegexConstants.Password);
        }

        public static bool IsMatchName(string input) {
            return input != null && Regex.IsMatch(input, RegexConstants.Name);
        }
    }
}
