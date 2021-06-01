import { AppState } from '../AppState'
import { api } from '../AxiosService'

class VaultsService {
  async createVault(v) {
    const res = await api.post('/api/vaults', v)
    AppState.vaults.push(res.data)
  }
}
export const vaultsService = new VaultsService()
