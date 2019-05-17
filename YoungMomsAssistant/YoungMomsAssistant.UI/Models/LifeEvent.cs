using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoungMomsAssistant.UI.Infrastructure.Extensions;

namespace YoungMomsAssistant.UI.Models {
    public class LifeEvent : ModelBase, IDataErrorInfo {
        private byte[] _image;
        private string _title;
        private string _summary;

        public int Id { get; set; }

        public string Title {
            get => _title;
            set {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Summary {
            get => _summary;
            set {
                _summary = value;
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

        public string Error => _errors.Values.FirstOrDefault(e => !string.IsNullOrWhiteSpace(e));

        public string this[string columnName] {
            get {
                var error = string.Empty;
                switch (columnName) {
                    case "Title":
                        if (string.IsNullOrWhiteSpace(Title) || Title.Length > 255) {
                            // TODO: Move to constants/localization
                            error = "error";
                            _errors["Title"] = error;
                        }
                        else {
                            _errors["Title"] = null;
                        }
                        break;
                    case "Summary":
                        if (Summary != null && Summary.Length > 1000) {
                            // TODO: Move to constants/localization
                            error = "error";
                            _errors["Summary"] = error;
                        }
                        else {
                            _errors["Summary"] = null;
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

                return error;
            }
        }
    }
}
