namespace Keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkr;
  private readonly VaultsRepository _vr;
  private readonly KeepsRepository _kr;
  private readonly KeepsService _ks;

  public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr, KeepsRepository kr, KeepsService ks)
  {
    _vkr = vkr;
    _vr = vr;
    _kr = kr;
    _ks = ks;
  }

  internal VaultKeep AddKeepToVault(VaultKeep vaultKeepData, string userId)
  {
    Vault foundVault = _vr.GetVault(vaultKeepData.VaultId);
    if (foundVault.CreatorId != userId)
    {
      throw new Exception("Vault does not belong to you");
    }
    var vaultKeep = _vkr.AddKeepToVault(vaultKeepData);
    var keep = _ks.GetById(vaultKeepData.KeepId);
    keep.Kept++;
    keep = _kr.UpdateKeep(keep);
    // I need to go and get the original keep that I'm trying to save
    // after finding the keep, increment its views
    // save the change in the database! update the keep in the repository
    return vaultKeep;
  }

  internal List<KeptKeeps> GetKeepsInVault(int vaultId, string userId)
  {
    Vault foundVault = _vr.GetVault(vaultId);
    if (foundVault.IsPrivate == true && foundVault.CreatorId != userId)
    {
      throw new Exception("Vault is set to private");
    }
    return _vkr.GetKeepsInVault(vaultId);
  }

  internal void DeleteKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = _vkr.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception("VaultKeep Id does not exist");
    }
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("This vaultKeep does not belong to you");
    }
    _vkr.DeleteKeep(vaultKeep);
    var keep = _ks.GetById(vaultKeep.KeepId);
    keep.Kept--;
    keep = _kr.UpdateKeep(keep);
  }
}