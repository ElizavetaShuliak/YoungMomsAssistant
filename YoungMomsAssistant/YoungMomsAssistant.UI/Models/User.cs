using System.ComponentModel;
using System.Linq;
using YoungMomsAssistant.UI.Infrastructure.Extensions;

namespace YoungMomsAssistant.UI.Models {
    public class User : ModelBase, IDataErrorInfo {

        private string _login;
        private string _email;
        private string _password;

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

        public string Error => _errors.Values.FirstOrDefault(e => !string.IsNullOrWhiteSpace(e));

        public string this[string columnName] {
            get {
                var error = string.Empty;
                switch (columnName) {
                    case "Login":
                        if (!RegexExtansions.IsMatchLogin(Login)) {
                            // TODO: Move to constants/localization
                            error = "error";
                            _errors["Login"] = error;
                        }
                        else {
                            _errors["Login"] = null;
                        }
                        break;
                    case "Email":
                        if (!RegexExtansions.IsMatchEmail(Email)) {
                            // TODO: Move to constants/localization
                            error = "error";
                            _errors["Email"] = error;
                        }
                        else {
                            _errors["Email"] = null;
                        }
                        break;
                    case "Password":
                        if (!RegexExtansions.IsMatchPassword(Password)) {
                            // TODO: Move to constants/localization
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

    }
}
