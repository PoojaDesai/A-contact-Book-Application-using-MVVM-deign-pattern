﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TestApplication {
    public class ViewModelBase : INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    }
}
