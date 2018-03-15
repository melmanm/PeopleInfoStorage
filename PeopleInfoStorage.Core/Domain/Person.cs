using PeopleInfoStorage.Core.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PeopleInfoStorage.Core.Domain
{
    public class Person : BaseEntity, IDataErrorInfo, INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public UInt16 HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        private DateTime _DayofBirth = DateTime.Parse("1980-01-01");
        public DateTime DayofBirth
        {
            get { return _DayofBirth; }
            set
            {
                _DayofBirth = value;
                NotifyPropertyChanged("Age");
            }
        }
        public UInt16 Age {
            get
            {
                return (UInt16)((DateTime.Today - _DayofBirth).TotalDays / 365.25);
            }
        }
        public bool IsValid()
        {
            var properties = this.GetType().GetProperties();
            
            foreach (var property in properties)
            {
                if (!String.IsNullOrEmpty(this[property.Name]))
                {
                    return false;
                }
            }
            return true;
        }
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "FirstName": FirstName.GetErrorIfEmpty(ref result); break;
                    case "LastName": LastName.GetErrorIfEmpty(ref result); break;
                    case "StreetName": StreetName.GetErrorIfEmpty(ref result); break;
                    case "PostalCode": PostalCode.GetErrorIfNotPostalCode(ref result); break;
                    case "PhoneNumber": PhoneNumber.GetErrorIfNotPhoneNumber(ref result); break;
                }              
                return result;
            }
        }


        public Person()
        { }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
