<template>
  <main class="container-fluid">
    <div class="row justify-content-center  my-3">
      <div class="card col-md-3 text-shadow bgimg text-light" :style="{ backgroundImage: `url(${vault?.img})` }">
        <div class="card-body text-center d-flex flex-column justify-content-end no-pad">
          <h1 class="caps">{{ vault?.name }}</h1>
          <h6><i>By {{ vault.creator?.name }}</i></h6>
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-2 text-center round bg-light">
        <h4>{{ keeps.length }} Keeps</h4>
      </div>
      <div class="row justify-content-center">
        <div class="col-sm-6 col-md-3 my-4" v-for="k in keeps" :key="k.id">
          <VaultKeepCard :keeps="k" />
        </div>
      </div>
    </div>
  </main>
</template>


<script>
import { computed, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState";
import VaultKeepCard from "../components/VaultKeepCard.vue";
import { vaultKeepsService } from "../services/VaultKeepsService";
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.id);
      } catch (error) {
        logger.error(router.push({ name: 'Home' }))
        Pop.error(error, "Get Vault By Id")
      }
    }

    async function getKeepsByVaultId() {
      try {
        await vaultKeepsService.getKeepsByVaultId(route.params.id);
      }
      catch (error) {
        Pop.error(error, "Getting Vault Keeps By Id");
      }
    }

    onMounted(() => {
      getKeepsByVaultId();
      getVaultById();
    });

    return {
      keeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
    };
  },
  components: { VaultKeepCard }
}
</script>


<style lang="scss" scoped>
.round {
  border-radius: 2rem;
  width: 15vh;
}

.no-pad {
  padding: 0%;
}

.caps {
  text-transform: uppercase;
  letter-spacing: 5px;
}

.bgimg {
  height: 25vh;
  width: 65vh;
  background-position: center;
  background-size: cover;
}

.outline {
  border: none;
  background-color: #ffffff00
}

.card-shadow {
  box-shadow: 0 2px 10px -1px #00000033, 0 5px 10px 0 #00000070;
}
</style>