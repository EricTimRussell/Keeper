namespace Keeper.Repositories;

public class ProfilesRepositiory : BaseRepository
{
  public ProfilesRepositiory(IDbConnection db) : base(db)
  {
  }

  internal Profile GetProfile(string profileId)
  {
    var sql = @"
   SELECT
   *
   FROM accounts 
   WHERE id = @profileId
   ;";
    return _db.QueryFirstOrDefault<Profile>(sql, new { profileId });
  }
}