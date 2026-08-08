using SocialNetworkAnalysis.Application.Models;

namespace SocialNetworkAnalysis.Presentation.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Degree { get; set; }
        public List<User> Friends {get; set;} 
    }
}
