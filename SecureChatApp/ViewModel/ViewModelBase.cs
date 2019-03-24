using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Event_and_Notify
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string monPropriete)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(monPropriete));
            }
        }

        public bool NotifyPropertyChanged<T>(ref T variable, T valeur, string monPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            NotifyPropertyChanged(monPropriete);
            return true;
        }
        #endregion
    }
}
