using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TestApplication.ViewModel {
   public class SelectViewModel:ISelectContentControl {
       private readonly MainViewModel _mainVM;
       private readonly CreateViewModel _createVM;
       private  LoadViewModel _loadVM;
       public MainViewModel MainVM { get { return _mainVM; } }

       public SelectViewModel(MainViewModel main) {
           this._mainVM = main;
           _createVM = new CreateViewModel();
         
       }


       private RelayCommand _createCommand;
       public ICommand CreateCommand {
           get {
               if (_createCommand == null) {
                   _createCommand = new RelayCommand(param => SetCreateAsControl());
               }
               return _createCommand;
           }
       }

       private void SetCreateAsControl() {
           MainVM.ViewContentControl = _createVM;
       }

       private RelayCommand _loadCommand;
       public ICommand LoadCommand {
           get {
               if (_loadCommand == null) {
                   _loadCommand = new RelayCommand(param => SetLoadAsControl());
               }
               return _loadCommand;
           }
       }

       private void SetLoadAsControl() {
           _loadVM = new LoadViewModel();
           MainVM.ViewContentControl = _loadVM;
       }

    }
}
