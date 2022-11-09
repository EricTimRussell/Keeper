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
        <p>{{ vault.length }} Vaults | #keeps</p>
      </div>
      <h3>Vaults</h3>
      <div class="col-sm-6 col-md-3 my-4" v-for="v in vault" :key="v.id">
        <VaultCard :vault="v" />
      </div>
      <div class="col-sm-12 col-md-8">
        <h3>Keeps</h3>
        <!-- keeps here -->
      </div>
    </div>
  </main>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from "vue-router"
import { AppState } from '../AppState'
import VaultCard from "../components/VaultCard.vue"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/vaultsService"
import Pop from "../utils/Pop"
export default {
  setup() {
    const route = useRoute();
    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults();
      }
      catch (error) {
        Pop.error(error, "Getting Account Vaults");
      }
    }

    async function getKeepById() {
      try {
        await keepsService.getKeepById()
      } catch (error) {
        Pop.error(error.message, "Getting keep by id")
      }
    }

    onMounted(() => {
      getMyVaults();
      // getKeepById()
    });
    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vaults)
    };
  },
  components: { VaultCard }
}
</script>

<style scoped>
.profile-img {
  border-radius: 50%;
}
</style>
