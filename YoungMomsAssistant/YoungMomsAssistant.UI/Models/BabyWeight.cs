using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Models {
    public class BabyWeight : ModelBase, IDataErrorInfo {
        private double _weight;
        private DateTime _date;
        private int _babyId;

        public int Id { get; set; }

        public int BabyId {
            get => _babyId;
            set {
                _babyId = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date {
            get => _date;
            set {
                _date = value;
                OnPropertyChanged();
            }
        }

        public double Weight {
            get => _weight;
            set {
                _weight = value;
                OnPropertyChanged();
            }
        }

        public string Error => _errors.Values.FirstOrDefault(e => !string.IsNullOrWhiteSpace(e));

        public string this[string columnName] {
            get {
                var error = string.Empty;
                switch (columnName) {
                    case "Date":
                        if (Date > DateTime.Now.Date) {
                            error = "error";
                            _errors["Date"] = error;
                        }
                        else {
                            _errors["Date"] = null;
                        }
                        break;
                    case "Weight":
                        if (Weight <= 0.0) {
                            error = "error";
                            _errors["Weight"] = error;
                        }
                        else {
                            _errors["Weight"] = null;
                        }
                        break;
                }

                return error;
            }
        }
    }
}
