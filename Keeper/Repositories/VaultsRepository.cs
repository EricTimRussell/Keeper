namespace Keeper.Repositories;

public class VaultsRepository : BaseRepository
{
  public VaultsRepository(IDbConnection db) : base(db)
  {
  }

  internal Vault CreateVault(Vault newVault)
  {
    var sql = @"
    INSERT INTO vaults(
      creatorId,
      name,
      description,
      img,
      isPrivate
    )
    VALUES(
      @CreatorId,
      @Name,
      @Description,
      @Img,
      @IsPrivate
    );
    SELECT LAST_INSERT_ID()
    ;";
    newVault.CreatedAt = DateTime.Now;
    newVault.UpdatedAt = DateTime.Now;
    int vaultId = _db.ExecuteScalar<int>(sql, newVault);
    newVault.Id = vaultId;
    return newVault;
  }

  internal Vault GetVault(int vaultId)
  {
    var sql = @"
    SELECT 
    v.*,
    a.*
    FROM vaults v 
    JOIN accounts a ON a.id = v.creatorId
    WHERE v.id = @vaultId
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { vaultId }).FirstOrDefault();
  }

  internal List<Vault> GetMyVaults(string creatorId)
  {
    string sql = @"
  SELECT
  v.*,
  a.*
  FROM vaults v
  JOIN accounts a ON a.id = v.creatorId
  WHERE a.id = @CreatorId
   ;";
    return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
     {
       v.Creator = a;
       return v;
     }, new { creatorId }).ToList();
  }

  internal List<Vault> GetVaultByProfile(string creatorId)
  {
    var sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE a.id = @CreatorId AND v.isPrivate = false
    ;";

    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { creatorId }).ToList();
  }

  internal Vault UpdateVault(Vault vault)
  {
    var sql = @"
     UPDATE vaults SET
     name = @Name,
     description = @Description,
     img = @Img,
     isPrivate = @IsPrivate
     WHERE id = @Id LIMIT 1
    ;";
    vault.UpdatedAt = DateTime.Now;
    var rows = _db.Execute(sql, vault);
    if (rows < 1)
    {
      throw new Exception("no changes made");
    }
    if (rows > 1)
    {
      throw new Exception("More than one row affected during edit");
    }
    return vault;
  }

  internal void DeleteVault(int vaultId)
  {
    _db.Execute("DELETE FROM vaults WHERE id = @vaultId", new { vaultId });
  }
}
