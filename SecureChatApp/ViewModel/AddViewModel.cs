using SecureChatApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SecureChatApp.ViewModel
{
    class AddViewModel: ViewModelBase
    {
        ObservableCollection<PersonneClass> myCollection;

        public AddViewModel(Frame frame/*ObservableCollection<PersonneClass> personnes*/)
        {
            //myCollection = new ObservableCollection<PersonneClass>();
            //myCollection = personnes;
            //DataContext = myCollection;
        }
    }
}
