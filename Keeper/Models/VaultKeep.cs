namespace Keeper.Models;

public class VaultKeep : DbItem<int>
{
  public string CreatorId { get; set; }

  // [Required]
  public int VaultId { get; set; }

  // [Required]
  public int KeepId { get; set; }
}
