import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfileById(id) {
    try {
      const res = await api.get(`api/profiles/${id}`)
      AppState.activeProfile = res.data
    } catch (err) {
      logger.error('Something went wrong', err)
    }
  }
}

export const profilesService = new ProfilesService()
