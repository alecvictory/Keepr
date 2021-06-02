import { AppState } from '../AppState'
import { api } from '../services/AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async getKeepById(id) {
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
  }

  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.keeps = res.data
  }

  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.keeps = res.data
  }

  async createKeep(data) {
    const res = await api.post('/api/keeps', data)
    this.getKeepsByProfileId(res.data.creatorId)
  }

  async updateKeep(id, newKeep) {
    await api.put(`api/keeps/${id}`, newKeep)
  }

  async removeKeep(id) {
    await api.delete(`api/keeps/${id}`)
  }
}
export const keepsService = new KeepsService()
