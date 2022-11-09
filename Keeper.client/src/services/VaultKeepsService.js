import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { api } from "./AxiosService";

class VaultKeepsService {
  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    console.log(res.data);
    AppState.keeps = res.data.map(k => new Keep(k))
  }
}
export const vaultKeepsService = new VaultKeepsService()