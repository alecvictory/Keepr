import { AppState } from '../AppState'
import { api } from '../services/AxiosService'

class VaultsService {
  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }

  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.vaults = res.data
  }

  async createVault(v) {
    const res = await api.post('/api/vaults', v)
    AppState.vaults.push(res.data)
  }

  async updateVault(id, newVault) {
    await api.put(`api/vaults/${id}`, newVault)
  }

  async removeVault(id) {
    await api.delete(`api/vaults/${id}`)
  }
}
export const vaultsService = new VaultsService()
