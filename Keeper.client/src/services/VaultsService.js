import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { api } from "./AxiosService"

class VaultsService {

  async createVault(formData) {
    const res = await api.post('api/vaults', formData)
    AppState.vaults.push(new Vault(res.data))
  }

  async getMyVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    // console.log(res.data);
    AppState.vaults = res.data.map(v => new Vault(v))
  }

  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    console.log(res.data);
    AppState.activeVault = res.data
  }
}
export const vaultsService = new VaultsService()