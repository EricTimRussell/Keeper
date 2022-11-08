namespace Keeper.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider _a0;
  private readonly VaultKeepsService _vks;

  public VaultKeepsController(Auth0Provider a0, VaultKeepsService vks)
  {
    _a0 = a0;
    _vks = vks;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> AddKeepToVault([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Bad user or token");
      }
      vaultKeepData.CreatorId = userInfo?.Id;
      VaultKeep addedVaultKeep = _vks.AddKeepToVault(vaultKeepData, userInfo?.Id);
      return Ok(addedVaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{vaultKeepId}")]
  public async Task<ActionResult<string>> DeleteKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      _vks.DeleteKeep(vaultKeepId, userInfo?.Id);
      return Ok("Vault Keep Removed");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}