import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { router } from "../router"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultsService {

  async createVault(formData) {
    const res = await api.post('api/vaults', formData)
    AppState.vaults.push(new Vault(res.data))
  }

  async getMyVaults() {
    const res = await api.get(`account/vaults`)
    // console.log(res.data);
    AppState.activeVault = res.data.map(v => new Vault(v))
  }

  async getUsersVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    console.log(res.data);
    AppState.activeVault = res.data.map(v => new Vault(v))
  }

  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    console.log(res.data);
    AppState.activeVault = res.data
  }

  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    AppState.activeVault = AppState.activeVault.filter(v => v.id != vaultId)
  }
}
export const vaultsService = new VaultsService()