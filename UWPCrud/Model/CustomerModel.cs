using Prism.Mvvm;

namespace UWPCrud.Model
{
    public class CustomerModel : BindableBase
    {

        private int _id;
        private string _firstName;
        private string _lastName;
        private int _age;
        private string _pesel;
        private string _occupation;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string FirstName
        {
            get => _firstName; 
            set => SetProperty(ref _firstName, value);
        }

        public int Age
        {
            get => _age; 
            set => SetProperty(ref _age, value);
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