using System.ComponentModel;
using System.Text.RegularExpressions;
using YoungMomsAssistant.UI.Constants;
using YoungMomsAssistant.UI.Infrastructure.Extensions;

namespace YoungMomsAssistant.UI.Models {
    public class SignInModel : ModelBase, IDataErrorInfo {

        private string _loginOrEmail;
        private string _password;
        private string _lastError;

        public string LoginOrEmail {
            get => _loginOrEmail;
            set {
                _loginOrEmail = value;
                OnPropertyChanged();
            }
        }

        public string Password {
            get => _password;
            set {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Error => _lastError;

        public string this[string columnName] {
            get {
                var error = string.Empty;

                switch (columnName) {
                    case "LoginOrEmail":
                        if (!RegexExtansions.IsMatchLogin(LoginOrEmail)
                            && !RegexExtansions.IsMatchEmail(LoginOrEmail)) {
                            // TODO: Move to constants/localization
                            error = "error";
                        }

                        break;
                    case "Password":
                        if (!RegexExtansions.IsMatchPassword(Password)) {
                            // TODO: Move to constants/localization
                            error = "error";
                        }

                        break;
                }

                _lastError = error;
                return error;
            }
        }

        public User ConvertToUser() {
            var regex = new Regex(RegexConstants.Email);
            var isEmail = regex.IsMatch(LoginOrEmail);

            return new User {
                Password = Password,
                Email = isEmail ? LoginOrEmail : string.Empty,
                Login = isEmail ? string.Empty : LoginOrEmail
            };
        }
    }
}
