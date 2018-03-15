using Caliburn.Micro;
using PeopleInfoStorage.Core.Domain;
using PeopleInfoStorage.Services.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PeopleInfoStorage.WPF.ViewModels
{
    public class MainWindowViewModel : Conductor<object>, IShell
    {
        private readonly IPeopleService _PeopleService;
        public MainWindowViewModel(IPeopleService peopleService)
        {
            _PeopleService = peopleService;
            People =  _PeopleService.GetPeopleAsync().Result;
        }


        private ICollection<Person> _People;
        public ICollection<Person> People
        {
            get { return _People; }
            set
            {
                _People = new BindableCollection<Person>(value);
                NotifyOfPropertyChange("People");
            }
        }

        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                _SelectedPerson = value;
                NotifyOfPropertyChange("SelectedPerson");
            }
        }

        public DateTime SelectedPersonDayofBirth
        {
            get { return _SelectedPerson.DayofBirth; }
            set
            {
                _SelectedPerson.DayofBirth = value;
                NotifyOfPropertyChange("SelectedPersonDayofBirth");
                NotifyOfPropertyChange("SelectedPersonAge");
            }
        }

        public void AddPerson()
        {
            People.Add(new Person());
            UpdateCanSave();
        }
        public void RemovePerson()
        {
            if (SelectedPerson != null)
            {
                People.Remove(SelectedPerson);
                UpdateCanSave();
            }
            
        }

        private bool _SaveControl;

        public async void UpdateCanSave()
        {
            _SaveControl = await _PeopleService.HasChangesAsync(People.ToList()); 

            NotifyOfPropertyChange(() => CanSave);
            NotifyOfPropertyChange(() => CanCancel);
        }
        public void CellEditEnding(object o)
        {
            UpdateCanSave();
        }


        public bool CanSave
        {
            get { return _SaveControl & People.ToList().TrueForAll(x => x.IsValid()); }
        }

        public bool CanCancel
        {
            get { return _SaveControl; }
        }
    
        public async void Save()
        {
            await _PeopleService.SaveChangesAsync(People.ToList());
            UpdateCanSave();
        }

        public async void Cancel()
        {
            People = await _PeopleService.GetPeopleAsync();
            UpdateCanSave();
        }

        private bool _AreOptionsVisible;
        public bool AreOptionsVisible
        {
            get { return _AreOptionsVisible; }
            set
            {
                _AreOptionsVisible = value;
                NotifyOfPropertyChange("AreOptionsVisible");
            }
        }
    }
}
