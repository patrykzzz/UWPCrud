using Prism.Mvvm;

namespace UWPCrud.Model
{
    public class CustomerModel : BindableBase
    {

        private string _firstName;
        private string _lastName;
        private string _pesel;
        private string _occupation;

        public string FirstName
        {
            get => _firstName; 
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string Pesel
        {
            get => _pesel;
            set => SetProperty(ref _pesel, value);
        }

        public string Occupation
        {
            get => _occupation;
            set => SetProperty(ref _occupation, value);
        }
    }
}