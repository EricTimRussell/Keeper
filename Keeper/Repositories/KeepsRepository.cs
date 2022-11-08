namespace Keeper.Repositories;

public class KeepsRepository : BaseRepository
{
  public KeepsRepository(IDbConnection db) : base(db)
  {
  }

  internal List<Keep> GetKeeps()
  {
    var sql = @"
    SELECT 
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    ;";

    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }).ToList();
  }

  public Keep GetById(int keepId)
  {
    var sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    WHERE k.id = @keepId
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { keepId }).FirstOrDefault();
  }

  internal Keep CreateKeep(Keep data)
  {
    var sql = @"
    INSERT INTO keeps(
      id,
      creatorId,
      name,
      description,
      img
    )
    VALUES(
      @Id,
      @CreatorId,
      @Name,
      @Description,
      @Img
    );
    SELECT LAST_INSERT_ID()
    ;";
    data.CreatedAt = DateTime.Now;
    data.UpdatedAt = DateTime.Now;
    data.Id = _db.ExecuteScalar<int>(sql, data);
    return data;
  }

  internal List<Keep> GetKeepByProfile(string creatorId)
  {
    var sql = @"
    SELECT
    *
    FROM keeps
    WHERE creatorId = @CreatorId
    ;";

    return _db.Query<Keep>(sql, new { creatorId }).ToList();
  }

  internal Keep UpdateKeep(Keep keep)
  {
    var sql = @"
    UPDATE keeps SET
     name = @Name,
     description = @Description,
     img = @Img
    WHERE id = @Id LIMIT 1
    ;";
    keep.UpdatedAt = DateTime.Now;
    var rows = _db.Execute(sql, keep);
    if (rows < 1)
    {
      throw new Exception("no changes made");
    }
    if (rows > 1)
    {
      throw new Exception("More than one row affected during edit");
    }
    return keep;
  }

  internal void DeleteKeep(int keepId)
  {
    var sql = "DELETE FROM keeps WHERE id = @KeepId LIMIT 1;";
    var rows = _db.Execute(sql, new { keepId });
    if (rows != 1)
    {
      throw new Exception("data was not deleted Id may be bad or data is bad");
    }
    return;
  }
}