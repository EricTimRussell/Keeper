import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { VaultKeep } from "../models/VaultKeep";
import { api } from "./AxiosService";

class VaultKeepsService {
  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    console.log(res.data);
    AppState.vaultKeeps = res.data.map(k => new Keep(k))
  }

  async addKeepToVault(vaultKeep) {
    const res = await api.post('api/vaultkeeps', vaultKeep)
    AppState.vaultKeeps.push(new VaultKeep(res.data))
    AppState.activeKeep.kept++
  }

  async removeKeepFromVault(keepId) {
    const res = await api.delete(`api/vaultkeeps/${keepId}`)
    AppState.vaultKeeps.filter(v => v.id != keepId)
  }
}
export const vaultKeepsService = new VaultKeepsService()