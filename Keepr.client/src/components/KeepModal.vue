<template>
  <div class="keep-modal container-fluid
       modal"
       v-if="state.activeKeep"
       id="keep-modal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="row">
          <div class="col">
            <img :src="state.activeKeep.img" alt="" class="img-fluid">
          </div>
          <div class="col">
            <div class="row">
              <div class="col">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
            </div>
            <div class="row">
              <div class="col d-flex justify-content-center">
                <i class="fa fa-eye"></i><span class="">{{ state.activeKeep.views }}</span>
                <i class="fab fa-kaggle"></i><span class="">{{ state.activeKeep.keeps }}</span>
                <i class="fa fa-share"></i><span class="">{{ state.activeKeep.shares }}</span>
              </div>
            </div>
            <div class="row">
              <div class="col d-flex justify-content-center">
                <h2>{{ state.activeKeep.name }}</h2>
              </div>
            </div>
            <div class="row">
              <div class="col d-flex justify-content-center">
                <p>{{ state.activeKeep.description }}</p>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" data-dismiss="modal" @click="removeKeep">
            Delete
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Notification from '../utils/Notification'
import { useRouter } from 'vue-router'
// import $ from 'jquery'

export default {
  name: 'KeepModal',
  setup() {
    const router = useRouter()
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep)
    })
    return {
      state,
      router,
      async removeKeep() {
        try {
          if (await Notification.confirmAction('Are you sure?', "You won't be able to revert this!", 'warning', 'Yes, Remove Keep')) {
            await keepsService.removeKeep(state.activeKeep.id)
            Notification.toast('Successfully Deleted Keep', 'success')
          }
        } catch (error) {
          Notification.toast('Error: ' + error, 'warning')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
