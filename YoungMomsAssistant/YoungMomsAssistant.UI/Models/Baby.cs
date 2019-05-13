using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Models {
    public class Baby : ModelBase {
        private string _firstName;
        private string _lastName;
        private DateTime _birthDay;
        private string _sex;
        private string _bloodType;

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
    }
}
