import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { VaultKeep } from "../models/VaultKeep";
import { api } from "./AxiosService";

class VaultKeepsService {
  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    console.log(res.data);
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async addKeepToVault(vaultKeep) {
    const res = await api.post('api/vaultkeeps', vaultKeep)
    AppState.vaultKeeps.push(new VaultKeep(res.data))
  }

}
export const vaultKeepsService = new VaultKeepsService()