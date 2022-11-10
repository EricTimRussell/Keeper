<template>
  <main class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-sm-12 col-md-8 d-flex flex-column justify-content-end align-items-center bgimg"
        :style="{ backgroundImage: `url(${account.coverImg})` }">
        <img :src=account.picture alt="profile pic" class="profile-img">
      </div>
      <div class="col-12 d-flex justify-content-center">
        <h1>{{ account.name }}</h1>
      </div>
      <div class="col-12 d-flex justify-content-center">
        <h6>{{ vault.length }} Vaults | {{ keeps.length }} Keeps</h6>
      </div>
      <h3>Vaults</h3>
      <div class="col-sm-6 col-md-3 my-4" v-for="v in vault" :key="v.id">
        <VaultCard :vault="v" />
      </div>
      <h3>Keeps</h3>
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

    // async function getMyVaults() {
    //   try {
    //     await vaultsService.getMyVaults();
    //   }
    //   catch (error) {
    //     Pop.error(error, "Getting Account Vaults");
    //   }
    // }

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
      // getMyVaults();
      getUsersKeeps();
      getUsersVaults()
    });

    return {
      account: computed(() => AppState.activeProfile),
      vault: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { VaultCard, KeepCard }
}
</script>


<style lang="scss" scoped>
.profile-img {
  border-radius: 50%;
  max-height: 12vh;
  min-height: 12vh;
  max-width: 12vh;
  min-width: 12vh;
}

.bgimg {
  height: 25vh;
  width: 65vh;
  background-position: center;
  background-size: cover;
}
</style>