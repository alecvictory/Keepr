<template>
  <div v-if="state.loading === true">
    <h1>Loading...</h1>
  </div>
  <div class="container-fluid">
    <div class="row">
      <div class="col">
        <div class="card-columns m-2">
          <Keep v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Home',
  setup() {
    const route = useRoute()
    const state = reactive({
      loading: true,
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    onMounted(async() => {
      try {
        await keepsService.getAllKeeps()
        state.loading = false
      } catch (error) {
        Notification.toast('error:' + error, 'danger')
      }
    })
    return {
      route,
      state
    }
  }
}
</script>

<style scoped lang="scss">
</style>
