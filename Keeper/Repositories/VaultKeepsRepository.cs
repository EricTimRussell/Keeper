namespace Keeper.Repositories;

public class VaultKeepsRepository : BaseRepository
{

  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }

  internal VaultKeep AddKeepToVault(VaultKeep vaultKeepData)
  {
    var sql = @"
    INSERT INTO vaultKeeps(
    keepId,
    creatorId,
    vaultId
  )
  VALUES(
    @KeepId,
    @CreatorId,
    @VaultId
  );
  SELECT LAST_INSERT_ID()
    ;";
    int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
    vaultKeepData.Id = id;

    return vaultKeepData;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
    SELECT
    vk.*,
    a.*
    FROM vaultKeeps vk
    JOIN accounts a ON a.id = vk.creatorId
    WHERE vk.id = @vaultKeepId
    ;";
    return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, p) =>
    {
      vk.CreatorId = p.Id;
      return vk;
    }, new { vaultKeepId }).FirstOrDefault();
  }

  internal List<KeptKeeps> GetKeepsInVault(int vaultId)
  {
    var sql = @"
   SELECT
    k.*,
    vk.id AS vaultKeepId,
    a.*
   FROM vaultKeeps vk
   JOIN keeps k ON k.id = vk.keepId
   JOIN accounts a ON a.id = vk.creatorId
   WHERE vk.vaultId = @VaultId
    ;";

    return _db.Query<KeptKeeps, Profile, KeptKeeps>(sql, (vk, p) =>
    {
      vk.Creator = p;
      return vk;
    }, new { vaultId }).ToList();
  }

  internal void DeleteKeep(VaultKeep vaultKeep)
  {
    string sql = @"DELETE FROM vaultKeeps WHERE id = @Id LIMIT 1;";
    _db.Execute(sql, vaultKeep);
  }

}