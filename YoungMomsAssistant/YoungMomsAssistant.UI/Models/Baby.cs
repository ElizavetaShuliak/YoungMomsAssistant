using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Infrastructure.Extensions;

namespace YoungMomsAssistant.UI.Models {
    public class Baby : ModelBase, IDataErrorInfo {

        public static string[] BloodTypes { get; set; } = {
            "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-"
        };

        private string _firstName;
        private string _lastName;
        private DateTime _birthDay;
        private string _sex;
        private string _bloodType;
        private byte[] _image;

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

        public bool IsImageChanged { get; set; }

        public byte[] Image {
            get => _image;
            set {
                _image = value;
                IsImageChanged = true;
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
                            _errors["FirstName"] = error;
                        }
                        else {
                            _errors["FirstName"] = null;
                        }
                        break;
                    case "LastName":
                        if (!RegexExtansions.IsMatchName(LastName)) {
                            error = "error";
                            _errors["LastName"] = error;
                        }
                        else {
                            _errors["LastName"] = null;
                        }
                        break;
                    case "Sex":
                        if (string.IsNullOrWhiteSpace(Sex)) {
                            error = "error";
                            _errors["Sex"] = error;
                        }
                        else {
                            _errors["Sex"] = null;
                        }
                        break;
                    case "BloodType":
                        if (string.IsNullOrWhiteSpace(BloodType)) {
                            error = "error";
                            _errors["BloodType"] = error;
                        }
                        else {
                            _errors["BloodType"] = null;
                        }
                        break;
                    case "Image":
                        if (Image == null) {
                            error = "error";
                            _errors["Image"] = error;
                        }
                        else {
                            _errors["Image"] = null;
                        }
                        break;
                }
                Error = "";
                return error;
            }
        }

        public string Error {
            get => _errors.Values.FirstOrDefault(e => !string.IsNullOrWhiteSpace(e));
            set {
                OnPropertyChanged();
            }
        }
    }
}
