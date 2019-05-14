using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Infrastructure.Extensions;

namespace YoungMomsAssistant.UI.Models {
    public class Baby : ModelBase, IDataErrorInfo {
        private string _firstName;
        private string _lastName;
        private DateTime _birthDay;
        private string _sex;
        private string _bloodType;

        private string _lastError;

        public int Id { get; set; }

        public string FirstName {
            get => _firstName;
            set {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName {
            get => _lastName;
            set {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDay {
            get => _birthDay;
            set {
                _birthDay = value;
                OnPropertyChanged();
            }
        }

        public string Sex {
            get => _sex;
            set {
                _sex = value;
                OnPropertyChanged();
            }
        }

        public string BloodType {
            get => _bloodType;
            set {
                _bloodType = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName] {
            get {
                var error = string.Empty;

                switch (columnName) {
                    case "FirstName":
                        if (!RegexExtansions.IsMatchName(FirstName)) {
                            error = "error";
                        }
                        break;
                    case "LastName":
                        if (!RegexExtansions.IsMatchName(LastName)) {
                            error = "error";
                        }
                        break;
                }

                return Error = error;
            }
        }

        public string Error {
            get => _lastError;
            set {
                _lastError = value;
                OnPropertyChanged();
            }
        }
    }
}
