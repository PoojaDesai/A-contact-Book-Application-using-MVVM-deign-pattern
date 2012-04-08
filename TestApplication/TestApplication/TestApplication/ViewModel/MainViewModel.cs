using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace TestApplication.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {

      public MainViewModel() {
          SelectContentControl = new SelectViewModel(this);
      }

      private ISelectContentControl _selectContentControl;
      public ISelectContentControl SelectContentControl {
          get { return _selectContentControl; }
          set {
              if (_selectContentControl == value)
                  return;

              if (_selectContentControl != null) {
                  CheckAndDispose(_viewContentControl);
                  _selectContentControl = null;
                  ViewContentControl = null;
              }
              _selectContentControl = value;
              OnPropertyChanged(() => SelectContentControl);
          }
      }

      private IViewContentControl _viewContentControl;
      public IViewContentControl ViewContentControl {
          get { return _viewContentControl; }
          set {
              if (_viewContentControl == value)
                  return;

              if (_viewContentControl != null) {
                  CheckAndDispose(_viewContentControl);
                  _viewContentControl = null;           
          }
              _viewContentControl=value;
              OnPropertyChanged(() => ViewContentControl);
      }
      }


      private static void CheckAndDispose(object component) {
          if (component is IDisposable) {
              (component as IDisposable).Dispose();
          }
      }
     
      
      // INotigyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;
      public void OnPropertyChanged(string propertyName) {
          if (PropertyChanged != null) {
              PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
          }
      }

      public void OnPropertyChanged<TResult>(Expression<Func<TResult>> propertyExpression) {
          OnPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
      }




    }
}
