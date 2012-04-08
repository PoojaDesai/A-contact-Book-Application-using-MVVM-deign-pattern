using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApplication.Model;
using System.Windows.Input;
using System.ComponentModel;

namespace TestApplication.ViewModel {
    public class CreateViewModel : ViewModelBase, IViewContentControl, IDataErrorInfo {
        private PersistenceManager PM;
        Person Person { get;  set; }    

     public CreateViewModel() {
         Person = new Person();
         PM = new PersistenceManager();
     }
     public int ID {
         get { return Person.ID; }
         set {
             if (Person.ID != value) {
                 Person.ID = value;
                 base.OnPropertyChanged("ID");
             }
         }
     }

     public string FirstName {
         get { return Person.Firstname; }
         set {
             if (Person.Firstname != value)
                 Person.Firstname = value;
                 base.OnPropertyChanged("FirstName");        
         }
     }


     public string ContactNumber {
         get { return Person.ContactNumber; }
         set {

             if (Person.ContactNumber != value) {
                 Person.ContactNumber = value;
                 base.OnPropertyChanged("ContactNumber");
              
             }
         }
     }

     public string EmailId {
         get { return Person.EmailId; }
         set { 
         
             if(Person.EmailId!=value){
                 Person.EmailId = value;
                 base.OnPropertyChanged("EmailID");             
             }
         }
     }

     public string Error {
         get { return (Person as IDataErrorInfo).Error; }
     }

     public string this[string propertyName] {
         get {
             string error = (Person as IDataErrorInfo)[propertyName];             
             CommandManager.InvalidateRequerySuggested();
             return error;
         }
     }


     private RelayCommand _saveCommand;
     public ICommand SaveCommand {
         get {
             if (_saveCommand == null) {
                 _saveCommand = new RelayCommand(param => SaveData());
             }
             return _saveCommand;
         }
     }

     private void SaveData() {        
         PM.Save<Person>(Person);     
     }


    }
}
