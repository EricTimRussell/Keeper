namespace Keeper.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkr;
  private readonly VaultsRepository _vr;

  public VaultKeepsService(VaultKeepsRepository vkr, VaultsRepository vr)
  {
    _vkr = vkr;
    _vr = vr;
  }

  internal VaultKeep AddKeepToVault(VaultKeep vaultKeepData, string userId)
  {
    Vault foundVault = _vr.GetVault(vaultKeepData.VaultId);
    if (foundVault.CreatorId != userId)
    {
      throw new Exception("Vault does not belong to you");
    }
    return _vkr.AddKeepToVault(vaultKeepData);
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
  }
}