namespace Domain.Models;

public class Subforum
{
   public long Id { get; set; }
   public User Owner { get; private set; }
   public string Title { get; private set; }
   public string Description { get; set; }

   public Subforum(User owner, string title)
   {
      Owner = owner;
      Title = title;
   }

   public Subforum()
   {
   }
}