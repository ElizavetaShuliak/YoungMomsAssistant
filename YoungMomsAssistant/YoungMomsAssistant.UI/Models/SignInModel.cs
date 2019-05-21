using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using YoungMomsAssistant.UI.Constants;
using YoungMomsAssistant.UI.Infrastructure.Extensions;

namespace YoungMomsAssistant.UI.Models {
    public class SignInModel : ModelBase, IDataErrorInfo {

        private string _loginOrEmail;
        private string _password;

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

        public string Error => _errors.Values.FirstOrDefault(e => !string.IsNullOrWhiteSpace(e));

        public string this[string columnName] {
            get {
                var error = string.Empty;

                switch (columnName) {
                    case "LoginOrEmail":
                        if (!RegexExtensions.IsMatchLogin(LoginOrEmail)
                            && !RegexExtensions.IsMatchEmail(LoginOrEmail)) {
                            error = "error";
                            _errors["LoginOrEmail"] = error;
                        }
                        else {
                            _errors["LoginOrEmail"] = null;
                        }

                        break;
                    case "Password":
                        if (!RegexExtensions.IsMatchPassword(Password)) {
                            error = "error";
                            _errors["Password"] = error;
                        }
                        else {
                            _errors["Password"] = null;
                        }

                        break;
                }
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
