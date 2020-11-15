using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XMLParser
{
    /// <summary>
    /// Base view model.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    class BaseViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged implementation

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
