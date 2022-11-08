<template>
  <header>
    <div>
      <Navbar />
    </div>
  </header>
  <main>
    <router-view />
  </main>

  <!-- create keep modal -->
  <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Create</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <CreateKeep />
        </div>
      </div>
    </div>
  </div>

</template>






<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import CreateKeep from "./components/CreateKeep.vue"
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateKeep }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
