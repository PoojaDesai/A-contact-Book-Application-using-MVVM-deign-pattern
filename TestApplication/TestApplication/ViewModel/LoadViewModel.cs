
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApplication.Model;
using Remotion.Linq.Collections;

namespace TestApplication.ViewModel {
   public class LoadViewModel:ViewModelBase,IViewContentControl {
   
       PersistenceManager _myModel = new PersistenceManager();
       private ObservableCollection<Person> _personData = new ObservableCollection<Person>();
       private IList<Person> PersonList = new List<Person>();
       private Person _selectedCustomer;


       public ObservableCollection<Person> PersonData {
           get { return _personData; }
           set {
               _personData = value;
               base.OnPropertyChanged("PersonData");
           }
       }


       public Person SelectedCustomer {
           get { return _selectedCustomer; }
           set {
               if (_selectedCustomer != value) {
                   _selectedCustomer = value;
                   OnPropertyChanged("SelectedCustomer");

               }
           }
       }

       public LoadViewModel() {         
           initializeload();
        }

       #region Load the DataBase
       public void initializeload() {

           PersonList = _myModel.RetrieveAll<Person>(SessionAction.BeginAndEnd);

           foreach (var item in PersonList) {
               PersonData.Add(new Person {
                   ID = item.ID,
                   Firstname = item.Firstname,
                 ContactNumber = item.ContactNumber,
                 EmailId= item.EmailId,
               });
           }
       }


       #endregion
    }
}
