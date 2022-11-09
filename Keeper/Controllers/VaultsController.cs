namespace Keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly Auth0Provider _a0;
  private readonly VaultKeepsService _vks;

  public VaultsController(VaultsService vs, VaultKeepsService vks, Auth0Provider a0)
  {
    _vs = vs;
    _a0 = a0;
    _vks = vks;
  }
  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Bad user or token");
      }
      newVault.CreatorId = userInfo?.Id;
      Vault createdVault = _vs.CreateVault(newVault);
      createdVault.Creator = userInfo;
      return Ok(createdVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public async Task<ActionResult<Vault>> GetVault(int vaultId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      Vault foundVault = _vs.GetVault(vaultId, userInfo?.Id);
      return Ok(foundVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<Vault>> GetKeepsInVault(int vaultId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      List<KeptKeeps> keptKeeps = _vks.GetKeepsInVault(vaultId, userInfo?.Id);
      return Ok(keptKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{vaultId}")]
  public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault vaultData, int vaultId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      vaultData.Id = vaultId;
      Vault vault = _vs.UpdateVault(vaultData, userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [Authorize]
  [HttpDelete("{vaultId}")]
  public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      _vs.DeleteVault(vaultId, userInfo?.Id);
      return Ok("Vault Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }




}