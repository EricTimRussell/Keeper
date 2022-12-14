namespace Keeper.Services;
public class KeepsService
{
  private readonly KeepsRepository _kr;

  public KeepsService(KeepsRepository kr)
  {
    _kr = kr;
  }

  public List<Keep> GetKeeps()
  {
    return _kr.GetKeeps();
  }

  public Keep GetById(int keepId)
  {
    Keep foundKeep = _kr.GetById(keepId);
    if (foundKeep == null)
    {
      throw new Exception("Keep does not exist");
    }
    foundKeep.Views++;
    _kr.UpdateKeep(foundKeep);
    return foundKeep;
  }

  public KeptKeeps getKeptKeepById(int id)
  {
    KeptKeeps keep = _kr.GetKeptKeepById(id);
    if (keep == null)
    {
      throw new Exception("KeptKeep not found");
    }
    return keep;
  }

  internal Keep CreateKeep(Keep data)
  {
    return _kr.CreateKeep(data);
  }

  internal Keep UpdateKeep(Keep keep, string userId)
  {
    Keep originalKeep = GetById(keep.Id);
    if (originalKeep.CreatorId != userId)
    {
      throw new Exception("This keep does not belong to you");
    }
    originalKeep.Name = keep.Name;
    originalKeep.Description = keep.Description;
    originalKeep.Img = keep.Img;
    originalKeep.Views = keep.Views;

    return _kr.UpdateKeep(originalKeep);
  }

  internal List<Keep> GetKeepByProfile(string creatorId)
  {
    return _kr.GetKeepByProfile(creatorId);
  }

  internal void DeleteKeep(int keepId, string userId)
  {
    var keep = GetById(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("This keep does not belong to you");
    }
    _kr.DeleteKeep(keepId);
  }
}