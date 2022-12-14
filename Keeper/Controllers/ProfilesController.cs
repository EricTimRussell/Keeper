namespace Keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _ps;
  private readonly Auth0Provider _a0;
  private readonly VaultsService _vs;
  private readonly KeepsService _ks;

  public ProfilesController(ProfilesService ps, VaultsService vs, KeepsService ks, Auth0Provider a0)
  {
    _a0 = a0;
    _ps = ps;
    _vs = vs;
    _ks = ks;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfile(string profileId)
  {
    try
    {
      Profile profile = _ps.GetProfile(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetKeepByProfile(string profileId)
  {
    try
    {
      var keeps = _ks.GetKeepByProfile(profileId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public ActionResult<List<Vault>> GetVaultByProfile(string profileId)
  {
    try
    {
      List<Vault> vault = _vs.GetVaultByProfile(profileId);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
