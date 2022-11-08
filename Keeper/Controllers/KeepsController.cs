namespace Keeper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly KeepsService _ks;
  private readonly Auth0Provider _a0;

  public KeepsController(KeepsService ks, Auth0Provider a0)
  {
    _ks = ks;
    _a0 = a0;
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetKeeps()
  {
    try
    {
      var keeps = _ks.GetKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      Keep foundKeep = _ks.GetById(keepId);
      return Ok(foundKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep newKeep)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Invalid user Id or Expired AuthToken");
      }
      newKeep.CreatorId = userInfo?.Id;
      Keep createdKeep = _ks.CreateKeep(newKeep);
      createdKeep.Creator = userInfo;
      return Ok(createdKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{keepId}")]
  public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep data, int keepId)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      data.Id = keepId;
      Keep keep = _ks.UpdateKeep(data, userInfo?.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{keepId}")]
  public async Task<ActionResult<string>> DeleteKeep(int keepId)
  {
    try
    {
      var userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      _ks.DeleteKeep(keepId, userInfo.Id);
      return Ok("Keep deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}