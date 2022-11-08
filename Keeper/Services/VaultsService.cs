namespace Keeper.Services;
public class VaultsService
{
  private readonly VaultsRepository _vr;
  private readonly ProfilesService _pr;

  public VaultsService(VaultsRepository vr, ProfilesService pr)
  {
    _vr = vr;
    _pr = pr;
  }

  internal Vault CreateVault(Vault newVault)
  {
    return _vr.CreateVault(newVault);
  }

  internal Vault GetVault(int vaultId)
  {
    Vault foundVault = _vr.GetVault(vaultId);
    if (foundVault == null)
    {
      throw new Exception("Vault does not exist");
    }
    if (foundVault.IsPrivate == true)
    {
      throw new Exception("Vault is private");
    }
    return foundVault;
  }

  internal Vault UpdateVault(Vault vaultData, string userId)
  {
    Vault originalVault = GetVault(vaultData.Id);
    if (originalVault.CreatorId != userId)
    {
      throw new Exception("This vault does not belong to you");
    }

    originalVault.Name = vaultData.Name;
    originalVault.Description = vaultData.Description;
    originalVault.Img = vaultData.Img;
    originalVault.IsPrivate = vaultData.IsPrivate;

    return _vr.UpdateVault(originalVault);
  }

  internal void DeleteVault(int vaultId, string userId)
  {
    var vault = GetVault(vaultId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("This vault does not belong to you");
    }
    _vr.DeleteVault(vaultId);
  }

  internal List<Vault> GetMyVaults(string creatorId)
  {
    return _vr.GetMyVaults(creatorId);
  }

  internal List<Vault> GetVaultByProfile(string creatorId)
  {
    if (creatorId == null)
    {
      throw new Exception("Profile does not exist");
    }
    var profile = _pr.GetProfile(creatorId);
    var profileVaults = _vr.GetVaultByProfile(creatorId);
    if (profile.Id != creatorId)
    {
      throw new Exception("Bad profileId or vault is private");
    }
    return profileVaults;
  }
}