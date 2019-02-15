using SecureChatApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.ViewModel
{
    class AddViewModel
    {
        ObservableCollection<PersonneClass> myCollection;

        public AddViewModel(ObservableCollection<PersonneClass> personnes)
        {
            myCollection = new ObservableCollection<PersonneClass>();
            myCollection = personnes;
            //DataContext = myCollection;
        }
    }
}
