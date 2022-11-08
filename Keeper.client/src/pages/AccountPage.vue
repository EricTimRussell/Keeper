<template>
  <main class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-sm-12 col-md-8 d-flex justify-content-center"
        :style="{ backgroundImage: `url(${account.coverImg})` }">
        <img :src=account.picture alt="profile pic" class="profile-img">
      </div>
      <div class="col-12 d-flex justify-content-center">
        <h1>{{ account.name }}</h1>
      </div>
      <div class="col-12 d-flex justify-content-center">
        <p>#vaults || #keeps</p>
      </div>
      <div class="col-sm-12 col-md-8">
        <h3>Vaults</h3>
        <!-- vaults here -->
      </div>
      <div class="col-sm-12 col-md-8">
        <h3>Keeps</h3>
        <!-- keeps here -->
      </div>
    </div>
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
.profile-img {
  border-radius: 50%;
}
</style>
