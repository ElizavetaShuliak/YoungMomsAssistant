using System.ComponentModel;
using YoungMomsAssistant.UI.Infrastructure.Extensions;

namespace YoungMomsAssistant.UI.Models {
    public class User : ModelBase, IDataErrorInfo {

        private string _login;
        private string _email;
        private string _password;
        private string _lastError;

        public string Login {
            get => _login;
            set {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Email {
            get => _email;
            set {
                _email = value;
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

        public string this[string columnName] {
            get {
                var error = string.Empty;
                switch (columnName) {
                    case "Login":
                        if (!RegexExtansions.IsMatchLogin(Login)) {
                            // TODO: Move to constants/localization
                            error = "error";
                        }
                        break;
                    case "Email":
                        if (!RegexExtansions.IsMatchEmail(Email)) {
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

        public string Error => _lastError;
    }
}
