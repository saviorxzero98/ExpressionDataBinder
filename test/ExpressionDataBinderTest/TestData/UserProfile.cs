namespace ExpressionDataBinder.Test.TestData
{
    public class UserProfile
    {
        public UserProfile()
        {

        }
        public UserProfile(string id, string name, int ages, string birth)
        {
            Id = id;
            Name = name;
            Ages = ages;
            Birth = birth;
        }

        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Ages { get; set; } = 0;

        public string Birth { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");

        public UserProfileDetial Detial { get; set; } = new UserProfileDetial();

        public bool IsBlocked { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }

    public class UserProfileDetial
    {
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public List<string> Interests { get; set; } = new List<string>();

        public List<string> Informations { get; set; } = new List<string>();
    }
}
