namespace YoungMomsAssistant.Core.Domain.Users.Utilities {
    static class UserUtility {

        public static string GetPasswordHash(string password) {
            return Utility.GetSHA256Hash(Utility.GetSHA256Hash(password));
        }
    }
}
