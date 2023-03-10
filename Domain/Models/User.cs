namespace Domain.Models;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public ICollection<Subforum> Subforums { get; set; }

}