using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Models {
    public class BabyGrowth : ModelBase, IDataErrorInfo {
        private double _growth;
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

        public double Growth {
            get => _growth;
            set {
                _growth = value;
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
                    case "Growth":
                        if (Growth <= 0.0) {
                            error = "error";
                            _errors["Growth"] = error;
                        }
                        else {
                            _errors["Growth"] = null;
                        }
                        break;
                }

                return error;
            }
        }
    }
}
