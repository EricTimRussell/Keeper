namespace Keeper.Models;

public class Vault : DbItem<int>
{
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public Boolean IsPrivate { get; set; }
}