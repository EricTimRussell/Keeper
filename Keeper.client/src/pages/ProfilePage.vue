<template>
  <main class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-sm-12 col-md-8 d-flex flex-column justify-content-end align-items-center bgimg my-3"
        :style="{ backgroundImage: `url(${account.coverImg})` }">
        <img :src=account.picture alt="profile pic" class="profile-img">
      </div>
      <div class="col-12 d-flex justify-content-center my-5">
        <h1>{{ account.name }}</h1>
      </div>
      <div class="col-12 d-flex justify-content-center bg-light round">
        <h6>{{ vault.length }} Vaults | {{ keeps.length }} Keeps</h6>
      </div>
      <div>
        <h1>Vaults</h1>
      </div>
      <div class="col-sm-6 col-md-3 my-4" v-for="v in vault" :key="v.id">
        <VaultCard :vault="v" />
      </div>
      <div class="bg-light elevation-2 my-3">
        <h1>Keeps</h1>
      </div>
      <div class="col-sm-6 col-md-3 my-4" v-for="k in keeps" :key="k.id">
        <KeepCard :keeps="k" />
      </div>
    </div>
  </main>
</template>


<script>
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { vaultsService } from "../services/VaultsService";
import VaultCard from "../components/VaultCard.vue";
import { computed } from "@vue/reactivity";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
import KeepCard from "../components/KeepCard.vue";
import { accountService } from "../services/AccountService";

export default {
  setup() {
    const route = useRoute();

    async function getUserProfile() {
      try {
        await accountService.getUserProfile(route.params.id)
      } catch (error) {
        Pop.error(error, "Getting user profile")
      }
    }

    async function getUsersVaults() {
      try {
        await vaultsService.getUsersVaults(route.params.id)
      } catch (error) {
        Pop.error(error, "Getting vault by userId")
      }
    }

    async function getUsersKeeps() {
      try {
        await keepsService.getUsersKeeps(route.params.id)
      } catch (error) {
        Pop.error(error, "Getting keep by userId")
      }
    }

    onMounted(() => {
      getUserProfile();
      getUsersKeeps();
      getUsersVaults()
      window.scrollTo(0, 0)
    });

    return {
      account: computed(() => AppState.activeProfile),
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { VaultCard, KeepCard }
}
</script>


<style lang="scss" scoped>
.round {
  border-radius: 2rem;
  width: 18vh;
}

.profile-img {
  border-radius: 50%;
  max-height: 12vh;
  min-height: 12vh;
  max-width: 12vh;
  min-width: 12vh;
  object-fit: cover;
  position: relative;
  top: 3.5rem;
}

.bgimg {
  height: 25vh;
  background-position: center;
  background-size: cover;
  border-radius: 1rem;
}
</style>