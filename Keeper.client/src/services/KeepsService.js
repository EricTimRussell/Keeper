import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

  async getAllKeeps() {
    const res = await api.get('api/keeps')
    // logger.log(res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async getUsersKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    // console.log(res.data);
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    AppState.keeps.push(new Keep(res.data))
  }

  async deleteKeep(id) {
    const res = await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(k => k.id != id)
  }

  async getKeepById(id) {
    const res = await api.get(`api/keeps/${id}`)
    // logger.log(res.data)
    AppState.activeKeep = res.data

  }

}
export const keepsService = new KeepsService()