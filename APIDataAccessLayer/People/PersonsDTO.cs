namespace APIDataAccessLayer.People
{
    public class PersonsDTO
    {
        
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public PersonsDTO()
        {
        }

        public PersonsDTO(int id, string nationalNo,string FName, string SName, string ThName, string LName, DateTime dateOfBirth,
            short gender, string address, string PhoneNumber, string email, int country,string imagePath)
        
        {
            this.PersonID = id;
            this.NationalNo = nationalNo;
            this.FirstName = FName;
            this.SecondName = SName;
            this.ThirdName = ThName;
            this.LastName = LName;
            this.DateOfBirth = dateOfBirth;
            this.Gendor =gender ;
            this.Address = address;

            this.Phone = PhoneNumber;
            this.Email = email;
            this.NationalityCountryID = country;
            this.ImagePath = imagePath;
        }

    }




}
