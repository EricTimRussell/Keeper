namespace Keeper.Services;

public class ProfilesService
{
  private readonly ProfilesRepositiory _pr;

  public ProfilesService(ProfilesRepositiory pr)
  {
    _pr = pr;
  }

  internal Profile GetProfile(string profileId)
  {
    Profile profile = _pr.GetProfile(profileId);
    if (profile == null)
    {
      throw new Exception("Profile id is bad");
    }
    return profile;
  }
}